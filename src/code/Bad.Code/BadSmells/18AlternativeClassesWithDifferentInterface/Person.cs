namespace Bad.Code.BadSmells._18AlternativeClassesWithDifferentInterface
{
    public class Person
    {
        public void SetContact(string telephone, string website, string address)
        {

        }
    }

    public class Company
    {
        public void Set(string telephone, string website, string address)
        {

        }
    }

    public class OrgUnit
    {
        public void SetAddress(string website, string address , string tel)
        {
        }
    }

    public class Employee
    {
        public void SetContactInfo(string mobile , string website, string address)
        {
        }
    }
}