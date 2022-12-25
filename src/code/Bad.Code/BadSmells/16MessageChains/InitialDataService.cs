using System.Collections.Generic;
using System.Linq;

namespace Bad.Code.BadSmells._16MessageChains
{
    public class InitialDataService
    {
        public void Initiate(InitialDataViewModel initialDataViewModel, string personnelId)
        {
            var personnelInitialData
                = initialDataViewModel
                    .TaxInitialData
                    .Where(x => x.PersonnelId == personnelId);

            //TODO
        }
    }

    public class InitialDataViewModel
    {
        public string Id { get; set; }

        public short Year { get; set; }

        public short Month { get; set; }

        public List<TaxInitialDataViewModel> TaxInitialData { get; set; }

    }


    public class TaxInitialDataViewModel
    {
        public string Id { get; set; }

        public string InitialDataId { get; set; }

        public string PersonnelId { get; set; }

        // درآمد مشمول مالیات سال تا ماه قبل
        public long TaxIncludedIncomeFromYear { get; set; }

        // (مالیات سال تا ماه قبل(مالیات مکسوره
        public long PreviousMonthlyConstantTax { get; set; }
    }
}
