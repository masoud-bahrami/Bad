using System;
using System.Collections.Generic;
using System.Linq;

namespace Bad.Code.BadSmells._06DivergentChange
{
    public enum EmployeeType
    {
        Hourly,
        Commission,
        Salaried
    }

    public enum PayDisposition
    {
        Mail, Paymaster, DirectDeposit
    }

    // DirectDeposit=account
    // salaried-employee = salary + MONTHLY!

    // commission-employee = base_pay + commission-rate + BI-WEEKLY
    // hourly-employee = hourly-rate + WEEKLY
    // sales-receipt = date + amount-sold;
    // time-card = date + hours-worked

    // commission rate + base salary , 4 weeks dubmit the sales Reciept
    internal class SalesItem
    {
        public DateTime SoldAt { get; set; }
        public int Amount { get; set; }
        public bool IsPayed { get; set; }
    }

    internal class TimeCard
    {
        public int Hours { get; set; }
        public DateTime TimeCardDate { get; set; }
        public bool IsPayed { get; set; }
    }

    internal class Employee
    {
        public List<SalesItem> SalesItems = new List<SalesItem>();
        public List<TimeCard> TimeCards = new List<TimeCard>();
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public decimal BaseRate { get; set; }
        public PayDisposition PayDisposition { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal Salary { get; set; }
        public void CalculateSalary()
        {
            Console.WriteLine("######### Welcome ###########");
            Console.WriteLine($"Date :{DateTime.Now}");
            Console.WriteLine($"{FirstName} {LastName}");

            decimal a = 0;
            decimal insurance = 0;
            decimal tax = 0;
            decimal net = 0;
            switch (EmployeeType)
            {
                case (EmployeeType.Hourly):
                    if (DateTime.Now.Day != 4)
                        throw new Exception();
                    int workedHours = TimeCards.Where(a => a.IsPayed == false)
                        .Sum(t => t.Hours);

                    int extraWorkedHours = workedHours % 8;

                    decimal salary = (workedHours / 8) * BaseRate + ((decimal)extraWorkedHours * ((decimal)1.4 * BaseRate));
                    a = salary;
                    net = a - insurance - tax;
                    break;

                case (EmployeeType.Commission):
                    if (DateTime.Now.Day != 4)
                        throw new Exception();

                    int soldT = this.SalesItems.Where(a => a.IsPayed == false)
                        .Sum(t => t.Amount);
                    var salary1 = CommissionRate * soldT;

                    a = salary1;
                    net = a - insurance - tax;
                    break;

                case (EmployeeType.Salaried):
                    if (DateTime.Now.Day != 4)
                        throw new Exception();

                    decimal salary2 = this.Salary;
                    a = salary2;
                    net = a - insurance - tax;
                    break;
            }

            Console.WriteLine($"Net Salary :{net}");
            Console.WriteLine($"Total :{a}");
            Console.WriteLine($"end");
        }


        public void Pay()
        {
            if (this.PayDisposition == PayDisposition.Mail)
            {
                // TODO
            }
            else if (this.PayDisposition == PayDisposition.Paymaster)
            {
                // TODO
            }
            else if (this.PayDisposition == PayDisposition.DirectDeposit)
            {
                // TODO
            }
        }
    }
}
