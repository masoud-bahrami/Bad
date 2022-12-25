using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Bad.Code.BadSmells._16MessageChains;

namespace Bad.Code.BadSmells.LargeClass
{
    public class TaxCalculationService 
    {
        private readonly IFormulaRepository _formulaRepository;
        private readonly ITaxTableRepository taxTableRepository;
        private readonly IPersonnelActionRepository _personnelActionRepository;
        private readonly IServiceEndRepository _serviceEndRepository;
        private readonly IPersonnelTaxRepository _personnelTaxRepository;
        private readonly ITaxCalculationRepository _calculateTaxRepository;
        private readonly IEventStore _eventStore;
        private readonly IDocumentStore _documentStore;
        private readonly ITaxConfigurationRepository _taxConfigurationRepository;
        private readonly IInsuranceCalculationRepository _insuranceCalculationRepository;
        private readonly ISalaryTaxDisketteItemRepository _salaryTaxDisketteItemRepository;

        public TaxCalculationService(IFormulaRepository formulaRepository,
            ITaxTableRepository taxTableRepository,
            IPersonnelActionRepository personnelActionRepository,
            IServiceEndRepository serviceEndRepository,
            ITaxCalculationRepository calculateTaxRepository,
            IPersonnelTaxRepository personnelTaxRepository,
            IEventStore eventStore,
            IDocumentStore documentStore,
            ITaxConfigurationRepository taxConfigurationRepository,
            IInsuranceCalculationRepository insuranceCalculationRepository,
            ISalaryTaxDisketteItemRepository salaryTaxDisketteItemRepository)
        {
            this._formulaRepository = formulaRepository;
            this.taxTableRepository = taxTableRepository;
            this._personnelActionRepository = personnelActionRepository;
            this._serviceEndRepository = serviceEndRepository;
            this._calculateTaxRepository = calculateTaxRepository;
            this._personnelTaxRepository = personnelTaxRepository;
            this._eventStore = eventStore;
            this._documentStore = documentStore;
            this._taxConfigurationRepository = taxConfigurationRepository;
            this._insuranceCalculationRepository = insuranceCalculationRepository;
            this._salaryTaxDisketteItemRepository = salaryTaxDisketteItemRepository;
        }

        public async Task<long> CalculateTax(Dictionary<string, long> salaryItems, string personnelId, short year, short month)
        {
            var constantTaxIds = (await this._formulaRepository.GetConstantTaxAsync()).Select(x => x.Title).ToList();
            var inConstantTaxIds = (await this._formulaRepository.GetInConstantTaxAsync()).Select(x => x.Title).ToList();

            var taxInitialData = await this.FetchTaxInitialData(personnelId, year, month);

            var salaryTaxDisketteItem = await this._salaryTaxDisketteItemRepository.Get(personnelId, year, month);
            long paymentInsuranceExemptionValue = 0;
            if (salaryTaxDisketteItem != null)
            {
                paymentInsuranceExemptionValue = (long)Math.Round(salaryTaxDisketteItem.PaymentInsurance);
            }

            // حق بیمه پرداختی موضوع ماده 137 ق.م.م

            // درآمد مستمر مشمول مالیات
            var sumOfConstantTaxSalaryItems = salaryItems.Where(x => constantTaxIds.Contains(x.Key)).Sum(x => x.Value);
            // درآمد غیرمستمر مشمول مالیات
            var sumOfInConstantTaxSalaryItems = salaryItems.Where(x => inConstantTaxIds.Contains(x.Key)).Sum(x => x.Value);
            //درآمد مشمول مالیات این ماه  
            var taxIncludedIncome = sumOfConstantTaxSalaryItems + sumOfInConstantTaxSalaryItems - paymentInsuranceExemptionValue;

            // درآمد مشمول مالیات سال تا ماه قبل
            var taxIncludedIncomeFromYear = await this._calculateTaxRepository.GetTaxIncludedIncomeFromYear(personnelId, year, month) + taxInitialData.TaxIncludedIncomeFromYear;

            // درآمد مستمر سال
            var yearlyConstantTaxIncome = taxIncludedIncome + taxIncludedIncomeFromYear;

            var recruitmentDate = await this.GetRecruitmentDate(personnelId, year, month);
            var pc = new PersianCalendar();

            // زمانی که شخص در سال های قبل استخدام شده باشد باید از اول سال محاسبه شود
            if (pc.GetYear(recruitmentDate) < year)
            {
                recruitmentDate = new DateTime(year, 1, 1, pc);
            }

            var recruitmentMonth = pc.GetMonth(recruitmentDate);

            // جمع روز های سال  پرسنل تا به حال
            var personnelDays = await this.SumOfPersonnelDays(personnelId, year, month, recruitmentDate);

            // کارکرد استاندارد سال تا به حال
            var yearlyStandardDays = this.YearlyStandardDays(recruitmentDate, year, month);

            // میانگین درآمد مستمر ماه
            var monthlyConstantTaxIncomeAverage = (yearlyConstantTaxIncome / personnelDays) * (yearlyStandardDays / (month - recruitmentMonth + 1));

            var taxGroupId = await this._personnelTaxRepository.GetByPersonnelId(personnelId, year, month);
            if (taxGroupId==null)
            {
                return 0;
            }

            var taxTableOutput = await this.taxTableRepository.GetTax(taxGroupId, monthlyConstantTaxIncomeAverage, year, month);

            // (مالیات کل مستمر (مالیات  سال تا به حال
            var constantTaxTotal = (taxTableOutput * (month - recruitmentMonth + 1) * personnelDays) / yearlyStandardDays;

            // (مالیات سال تا ماه قبل(مالیات مکسوره
            var previousMonthlyConstantTax = await this._calculateTaxRepository.GetPreviousMonthlyConstantTax(personnelId, year, month) + taxInitialData.PreviousMonthlyConstantTax;

            // مالیات مستمر ماه جاری
            var monthlyConstantTax = constantTaxTotal - previousMonthlyConstantTax;

            var saveCommand = new SaveTaxCalculationCommand(personnelId, year, month, taxIncludedIncome, taxIncludedIncomeFromYear,
                yearlyConstantTaxIncome, monthlyConstantTaxIncomeAverage, constantTaxTotal, monthlyConstantTax, previousMonthlyConstantTax,
                sumOfConstantTaxSalaryItems, sumOfInConstantTaxSalaryItems);

            var taxCalculationId = new TaxCalculationId(Guid.NewGuid().ToString());

            var taxCalculation = new TaxCalculation(taxCalculationId, saveCommand);

            await this._eventStore.AppendToEventStreamAsync(taxCalculation.Identity, taxCalculation.GetQueuedEvents());

            return monthlyConstantTax;
        }

        public async Task RemoveTaxCalculation(string personnelId, short year, short month)
        {
            try
            {
                var currentTaxCalculation = (await this._documentStore.FetchAll<TaxCalculationViewModel>(x =>
                    x.PersonnelId == personnelId && x.Year == year && x.Month == month)).FirstOrDefault();
                if (!currentTaxCalculation == null)
                {
                    var taxCalculationEvents = await this._eventStore.LoadEventStreamAsync(new TaxCalculationId(currentTaxCalculation.Id));
                    if (taxCalculationEvents.Payloads.Any())
                    {
                        var taxCalculation = new TaxCalculation(taxCalculationEvents.Events.Select(e => e.Payload).ToArray());

                        var deleteTaxCalculationCommand = new DeleteTaxCalculationCommand(taxCalculation.Identity.Id);

                        taxCalculation.Delete(deleteTaxCalculationCommand);

                        await this._eventStore.AppendToEventStreamAsync(taxCalculation.Identity,
                            taxCalculation.GetQueuedEvents().Select(AppendEventDto.Version1).ToList());
                    }
                }
            }
            catch
            {
                throw new DataMisalignedException("Tax Calculation didn't get deleted!");
            }
        }

        public async Task RemoveSalaryTaxDisketteItems(string personnelId, short year, short month)
        {
            try
            {
                var currentSalaryTaxDisketteItem = (await this._documentStore.FetchAll<SalaryTaxDisketteItemViewModel>(x =>
                    x.PersonnelId == personnelId && x.Year == year && x.Month == month)).FirstOrDefault();

                if (!currentSalaryTaxDisketteItem == null)
                {
                    var salaryTaxDisketteItemEvents = await this._eventStore.LoadEventStreamAsync(new SalaryTaxDisketteItemId(currentSalaryTaxDisketteItem.Id));
                    if (salaryTaxDisketteItemEvents.Payloads.Any())
                    {
                        var salaryTaxDisketteItem = new SalaryTaxDisketteItem(salaryTaxDisketteItemEvents.Events.Select(e => e.Payload).ToArray());

                        var deleteSalaryTaxDisketteItemCommand = new DeleteSalaryTaxDisketteItemCommand(salaryTaxDisketteItem.Identity.Id);

                        salaryTaxDisketteItem.Delete(deleteSalaryTaxDisketteItemCommand);

                        await this._eventStore.AppendToEventStreamAsync(salaryTaxDisketteItem.Identity, salaryTaxDisketteItem.GetQueuedEvents().Select(AppendEventDto.Version1).ToList());
                    }
                }
            }
            catch
            {
                throw new DataMisalignedException("salary Tax Diskette Item didn't get deleted!");
            }
        }

        public async Task CalculateSalaryTaxDisketteItems(Dictionary<string, long> salaryItems, string personnelId, short year, short month)
        {
            var taxConfiguration = await this.GetValue(salaryItems);

            if (!This.Is.NullOrEmpty(taxConfiguration))
            {
                var employeesInsuranceContribution = await this._insuranceCalculationRepository.GetEmployeesInsuranceContributionValue(personnelId, year, month);
                var paymentInsurance = taxConfiguration["PaymentInsurance"] * employeesInsuranceContribution;

                var saveCommand = new SaveSalaryTaxDisketteItemCommand(personnelId, year, month,
                    taxConfiguration["GrossContinuousCashSalary"],
                    taxConfiguration["OverdueContinuousPaymentsWithoutTax"],
                    taxConfiguration["AmountDeductedFromSalaryForHousing"],
                    taxConfiguration["AmountDeductedFromSalaryForVehicle"],
                    taxConfiguration["PaymentContinuousNonCashBenefits"],
                    taxConfiguration["TreatmentCosts"],
                    paymentInsurance,
                    taxConfiguration["HousingCreditFacilityFromBank"],
                    taxConfiguration["OtherExemptions"],
                    taxConfiguration["GrossOvertime"],
                    taxConfiguration["OtherNonContinuousCashPayments"],
                    taxConfiguration["CaseRewards"],
                    taxConfiguration["OverdueNonContinuousCashPayments"],
                    taxConfiguration["PaymentNonContinuousNonCashBenefits"],
                    taxConfiguration["NorouzBonusAndBenefitsEndYear"],
                    taxConfiguration["RedemptionLeaveAndYearsOfService"],
                    taxConfiguration["Exemption"],
                    taxConfiguration["ExemptionRelatedToFreeTradeZones"],
                    taxConfiguration["ExemptionFromLawOnAvoidingDoubleTaxation"],
                    taxConfiguration["NonContinuousCashExemptions"]);

                var salaryTaxDisketteItemId = new SalaryTaxDisketteItemId(Guid.NewGuid().ToString());

                var salaryTaxDisketteItem = new SalaryTaxDisketteItem(salaryTaxDisketteItemId, saveCommand);

                await this._eventStore.AppendToEventStreamAsync(salaryTaxDisketteItem.Identity,
                    salaryTaxDisketteItem.GetQueuedEvents().Select(AppendEventDto.Version1).ToList());
            }
        }

        public async Task<Dictionary<string, decimal>> GetValue(Dictionary<string, long> salaryItems)
        {
            var resultDict = new Dictionary<string, decimal>();

            var taxConfiguration = await this._taxConfigurationRepository.Get();

            if (taxConfiguration!=null)
            {
                foreach (var propertyInfo in taxConfiguration.GetType().GetProperties())
                {
                    if (propertyInfo.Name == "Id") continue;

                    var propertyValue = propertyInfo.GetValue(taxConfiguration, null)?.ToString();

                    if (string.IsNullOrEmpty(propertyValue))
                    {
                        resultDict.TryAdd(propertyInfo.Name, 0);
                        continue;
                    }

                    if (propertyInfo.Name == "PaymentInsurance")
                    {
                        var values = propertyValue.Split('/');
                        var factor = decimal.Parse(values.First()) / decimal.Parse(values.Last());
                        resultDict.TryAdd(propertyInfo.Name, factor);
                        continue;
                    }

                    var item = salaryItems.FirstOrDefault(x => x.Key == propertyValue);

                    if (item!=null)
                    {
                        resultDict.TryAdd(propertyInfo.Name, item.Value);
                    }
                }
            }

            return resultDict;
        }

        private async Task<TaxInitialDataViewModel> FetchTaxInitialData(string personnelId, short year, short month)
        {
            var initialData = (await this._documentStore.FetchAll<InitialDataViewModel>(x => x.Year == year && x.Month == month)).MaxBy(x => x.CreatedAt);
            if (initialData!=null)
            {
                var taxInitialDataViewModel = (await this._documentStore.FetchAll<TaxInitialDataViewModel>(x => x.InitialDataId == initialData.Id && x.PersonnelId == personnelId)).FirstOrDefault();
                if (taxInitialDataViewModel!=null)
                {
                    return taxInitialDataViewModel;
                }

                return new TaxInitialDataViewModel();
            }

            return new TaxInitialDataViewModel();
        }

        private async Task<DateTime> GetRecruitmentDate(string personnelId, short year, short month)
        {
            return await this._personnelActionRepository.GetRecruitmentDate(personnelId, year, month);
        }

        private async Task<int> SumOfPersonnelDays(string personnelId, short year, short month, DateTime recruitmentDate)
        {
            int result;
            var serviceEndDate = await this._serviceEndRepository.GetServiceEndDate(personnelId);

            if (serviceEndDate.Date > recruitmentDate.Date)
                result = Convert.ToInt32((serviceEndDate - recruitmentDate).TotalDays);
            else
            {
                var persianCalendar = new PersianCalendar();
                var monthDays = persianCalendar.GetDaysInMonth(year, month);
                var currentDate = new DateTime(year, month, monthDays, persianCalendar);
                result = Convert.ToInt32((currentDate - recruitmentDate).TotalDays + 1);
            }

            return result;
        }

        private int YearlyStandardDays(DateTime recruitmentDate, short year, short month)
        {
            int sumOfDays = 0;
            var persianCalendar = new PersianCalendar();

            var recruitmentMonth = persianCalendar.GetMonth(recruitmentDate);

            for (int i = recruitmentMonth; i <= month; i++)
            {
                sumOfDays += persianCalendar.GetDaysInMonth(year, i);
            }

            return sumOfDays;
        }
    }

    public class SalaryTaxDisketteItemViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string PersonnelId { get; set; }
    }

    public class TaxCalculationViewModel
    {
        public string PersonnelId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }

    public class AppendEventDto
    {
        public static TResult Version1<TResult>(object arg)
        {
            throw new NotImplementedException();
        }
    }

    public class TaxCalculation
    {
        public TaxCalculation(TaxCalculationId taxCalculationId, SaveTaxCalculationCommand saveCommand)
        {
            
        }

        public object Identity { get; set; }

        public List<object> GetQueuedEvents()
        {
            throw new NotImplementedException();
        }
    }

    public class TaxCalculationId
    {
        public string Id { get; }

        public TaxCalculationId(string id)
        {
            Id = id;
        }
    }

    public class SaveTaxCalculationCommand
    {
        public string PersonnelId { get; }
        public short Year { get; }
        public short Month { get; }
        public long TaxIncludedIncome { get; }
        public long TaxIncludedIncomeFromYear { get; }
        public long YearlyConstantTaxIncome { get; }
        public long MonthlyConstantTaxIncomeAverage { get; }
        public int ConstantTaxTotal { get; }
        public long MonthlyConstantTax { get; }
        public long PreviousMonthlyConstantTax { get; }
        public long SumOfConstantTaxSalaryItems { get; }
        public long SumOfInConstantTaxSalaryItems { get; }

        public SaveTaxCalculationCommand(string personnelId, short year, short month, long taxIncludedIncome, long taxIncludedIncomeFromYear, long yearlyConstantTaxIncome, long monthlyConstantTaxIncomeAverage, int constantTaxTotal, long monthlyConstantTax, long previousMonthlyConstantTax, long sumOfConstantTaxSalaryItems, long sumOfInConstantTaxSalaryItems)
        {
            PersonnelId = personnelId;
            Year = year;
            Month = month;
            TaxIncludedIncome = taxIncludedIncome;
            TaxIncludedIncomeFromYear = taxIncludedIncomeFromYear;
            YearlyConstantTaxIncome = yearlyConstantTaxIncome;
            MonthlyConstantTaxIncomeAverage = monthlyConstantTaxIncomeAverage;
            ConstantTaxTotal = constantTaxTotal;
            MonthlyConstantTax = monthlyConstantTax;
            PreviousMonthlyConstantTax = previousMonthlyConstantTax;
            SumOfConstantTaxSalaryItems = sumOfConstantTaxSalaryItems;
            SumOfInConstantTaxSalaryItems = sumOfInConstantTaxSalaryItems;
        }
    }

    public interface ISalaryTaxDisketteItemRepository
    {
        Task<SalaryTaskDeskette> Get(string personnelId, short year, short month);
    }

    public class SalaryTaskDeskette
    {
        public decimal PaymentInsurance { get; set; }
    }

    public interface IInsuranceCalculationRepository
    {
    }

    public interface ITaxConfigurationRepository
    {
    }

    public interface IDocumentStore
    {
        Task<List<T>> FetchAll<T>(Func<T, bool> func);
    }

    public interface IEventStore
    {
        Task AppendToEventStreamAsync(object identity, object toList);
        Task LoadEventStreamAsync(object id);
    }

    public interface ITaxCalculationRepository
    {
        Task<long> GetTaxIncludedIncomeFromYear(string personnelId, short year, short month);
        Task<long> GetPreviousMonthlyConstantTax(string personnelId, short year, short month);
    }

    public interface IPersonnelTaxRepository
    {
        Task<string> GetByPersonnelId(string personnelId, short year, short month);
    }

    public interface IServiceEndRepository
    {
    }

    public interface IPersonnelActionRepository
    {
    }

    public interface ITaxTableRepository
    {
        Task<int> GetTax(string taxGroupId, long monthlyConstantTaxIncomeAverage, short year, short month);
    }

    public interface IFormulaRepository
    {
        Task<List<ConstantTax>> GetConstantTaxAsync();
        Task<List<InConstantTax>> GetInConstantTaxAsync();
    }

    public class InConstantTax
    {
        public string Title { get; set; }
    }

    public class ConstantTax
    {
        public string Title { get; set; }
    }
}
