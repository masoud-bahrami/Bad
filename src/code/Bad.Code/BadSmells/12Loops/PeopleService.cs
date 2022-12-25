using System.Collections.Generic;

namespace Bad.Code.BadSmells._12Loops
{
    // Refactoring book
    public class PeopleService
    {
        public (int averageAge, int totalSalary) GetDetails(List<People> customers)
        {
            int averageAge = 0;
            int totalSalary = 0;
            int totalCustomers = 0;
            foreach (var customer in customers)
            {
                averageAge += customer.Age;
                totalSalary += customer.Salary;
                totalCustomers++;
            }
            averageAge = averageAge / totalCustomers;

            return (averageAge, totalSalary);
        }

        public string Report(List<People> customers)
        {
            int youngest = customers[0].Age != 0 ? customers[0].Age : int.MaxValue;
            int totalSalary = 0;
            
            for(int i = 1 ; i< customers.Count - 1 ; i++)
            {
                if(customers[i].Age < youngest)
                    youngest = customers[i].Age;

                totalSalary += customers[i].Salary;
            }

            return $"youngest age is {youngest} and totalSalary is {totalSalary}";
        }
    }


    public class People
    {
        public int Age { get; set; }
        public int Salary { get; set; }
    }
}