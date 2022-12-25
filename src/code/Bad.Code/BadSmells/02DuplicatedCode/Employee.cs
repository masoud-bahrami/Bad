namespace Bad.Code.BadSmells._02DuplicatedCode
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }

    public class Salesman : Employee
    {
        public string GetFulName()
        {
            return $"{FirstName} - {LastName}";
        }
    }
    public class Engineer : Employee
    {
        public string GetFulName()
        {
            return $"{FirstName} - {LastName}";
        }
    }
}