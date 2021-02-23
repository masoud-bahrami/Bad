namespace Bad.Code._10Primitive_Obsession
{
    
    public class EmployeeFactory
    {
        public Employee CreateEmployee(string name, EmployeeType type)
        {
            return new Employee { Name = name, Type = type };
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public EmployeeType Type { get; set; }
    }

    public enum EmployeeType
    {
        Engineer,
        SalesMan,
        Manager
    }
}