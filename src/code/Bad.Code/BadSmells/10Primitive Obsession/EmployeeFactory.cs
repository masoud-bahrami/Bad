using System;

namespace Bad.Code.BadSmells._10Primitive_Obsession
{

    public class EmployeeFactory
    {
        public Employee CreateEmployee(string name, string employeeType)
        {
            switch (employeeType)
            {
                case "Engineer":
                    return new Enginner(name);
                case "SalesMan":
                    return new SalesMan(name);
                case "Manager":
                    return new Manager(name);
                default:
                    throw new ArgumentOutOfRangeException("employeeType");
            }
        }
    }

    public class Manager : Employee
    {
        public Manager(string name) : base(name)
        {

        }
    }

    public class SalesMan : Employee
    {
        public SalesMan(string name) : base(name)
        {

        }
    }

    public class Enginner : Employee
    {
        public Enginner(string name) : base(name)
        {

        }
    }

    public abstract class Employee
    {
        protected Employee(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

}