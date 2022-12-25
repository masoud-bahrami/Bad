namespace Bad.Code.BadSmells._12Loops;

public class OfficeCsvService
{
    public void AcquireOfficeTelephoneInCountry(string input , string countryName)
    {
        var lines = input.Split("\n");
        bool firstLine = true;

        foreach (var line in lines)
        {
            if (firstLine)
            {
                firstLine = false;
                continue;
            }

            if(line.Trim() == "") continue;

            var record = line.Split(",");

            if (record[0].Trim() == countryName)
            {
                var foo = new OfficePhone
                {
                    City = record[0].Trim(),
                    Country = record[1].Trim(),
                    Phone = record[2].Trim()
                };
            }
        }
    }
}