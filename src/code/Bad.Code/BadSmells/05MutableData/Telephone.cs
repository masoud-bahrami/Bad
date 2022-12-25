namespace Bad.Code.BadSmells._05MutableData
{
    public class Telephone
    {
        public string OfficeAreaCode { get; set; }
        public string OfficeNumber { get; set; }
    }

    public class Employee
    {
        public Telephone Telephone { get; set; }

        public void ChangeOfficeAreaCode(string newOfficeAreaCode)
        {
            // first clear the old number!
            Telephone.OfficeAreaCode = string.Empty;
            Telephone.OfficeAreaCode = newOfficeAreaCode;
        }

        public void ChangeOfficeNumber(string newOfficeNumber)
        {
            // first clear the old number!
            Telephone.OfficeNumber = string.Empty;
            Telephone.OfficeNumber = newOfficeNumber;
        }

    }
}