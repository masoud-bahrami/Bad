using System;
using System.Threading.Tasks;

namespace Bad.Code.BadSmells._08FeatureEnvy
{
    public enum ExcessDeductionType
    {
        NA = 0,
        OnFirstInstallment = 1,
        OnLastInstallment = 2,
        WithFirstInstallment = 3,
        WithLastInstallment = 4,
    }

    public class LoanDesignation
    {
        public string Id
        {
            get;
            set;
        }

        public string PersonnelId { get; private set; }

        public string LoanTypeId { get; private set; }

        public DateTime PaymentDate { get; private set; }

        public DateTime InstallmentStartDate { get; private set; }

        public long TotalAmount { get; private set; }

        public long PaybackAmount { get; private set; }

        public long InstallmentAmount { get; set; }

        public int InstallmentCount { get; private set; }

        public long ExcessAmount { get; private set; }

        public ExcessDeductionType ExcessDeductionType { get; private set; }

        public long RemainAmount { get; private set; }
        public long ModificationAmount { get; set; }
    }

    public class LoanCalculationService
    {

        public async Task<long> CalculateLoan(string personnelId, short year, short month)
        {

            LoanDesignation loan = GetByPersonnelIdYearMonth(personnelId, year, month);

            if (loan == null)
                return 0;

            long installmentTotal = 0;

            var installmentRemainCount = loan.InstallmentCount;
            var installmentAmount = loan.InstallmentAmount;
            long remainAmount = 0;
            long excessAmount = loan.ExcessAmount;

            // TODO calculate the personnel loan
            return installmentTotal;
        }


        private LoanDesignation GetByPersonnelIdYearMonth(string personnelId, short year, short month)
        {
            //TODO fetch from database
            throw new NotImplementedException();
        }
    }
}