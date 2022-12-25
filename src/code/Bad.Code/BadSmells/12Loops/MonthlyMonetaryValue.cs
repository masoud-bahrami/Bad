using System.Collections.Generic;
using System.Linq;

namespace Bad.Code.BadSmells._12Loops
{
    public partial class MonthlyMonetaryValue 
    {

        private List<PersonnelMonthlyMonetary> personnelMonthlyMonetary;


        public void Update(UpdateMonthlyMonetaryValueCommand updateCommand)
        {
            foreach (var personnelMonthlyMonetary in updateCommand.MonthlyMonetaryValueCommandDto.PersonnelMonthlyMonetarys)
            {
                var oldPersonnel =
                    this.personnelMonthlyMonetary.FirstOrDefault(x => x.PersonnelId == personnelMonthlyMonetary.PersonnelId);

                if (oldPersonnel == null)
                {
                    //TODO create/add new personnel monetary value
                }
                else
                {
                    foreach (var valueOfMonthlyItem in personnelMonthlyMonetary.ValueOfMonthlyItems)
                    {
                        var oldMonthlyMonetaryData = oldPersonnel.ValueOfMonthlyMonetaryData.FirstOrDefault(x =>
                            x.MonthlyMonetaryDataId == valueOfMonthlyItem.MonthlyMonetaryDataId);

                        if (oldMonthlyMonetaryData == null)
                        {
                            // TODO update monetary values imported for the personnel
                        }
                        else
                        {
                            // TODO add  monetary values for the personnel
                        }
                    }
                }
            }
        }
        
    }


    public class PersonnelMonthlyMonetary 
    {
        public string PersonnelId { get; private set; }

        public List<ValueOfMonthlyMonetaryData> ValueOfMonthlyMonetaryData { get; private set; }
        
    }

    public class ValueOfMonthlyMonetaryData
    {
        public string Value { get; private set; }
        public string MonthlyMonetaryDataId { get; set; }
    }

    public class UpdateMonthlyMonetaryValueCommand
    {
        public string Id { get; set; }

        public MonthlyMonetaryValueCommandDto MonthlyMonetaryValueCommandDto { get; set; }
        
    }

    public class MonthlyMonetaryValueCommandDto
    {
        public short CurrentYear { get; set; }

        public short CurrentMonth { get; set; }
        public List<PersonnelMonthlyMonetaryCommandDto> PersonnelMonthlyMonetarys { get; set; }

    }

    public class PersonnelMonthlyMonetaryCommandDto
    {
        public string PersonnelId { get; set; }

        public List<ValueOfMonthlyMonetaryDataCommandDto> ValueOfMonthlyItems { get; set; }
    }

    public class ValueOfMonthlyMonetaryDataCommandDto
    {
        public string MonthlyMonetaryDataId { get; set; }

        public string Value { get; set; }
    }
}
