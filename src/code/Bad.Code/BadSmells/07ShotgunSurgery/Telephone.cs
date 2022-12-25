using System;

namespace Bad.Code.BadSmells._07ShotgunSurgery
{
    public class Telephone
    {
        public string Number { get; set; }
    }

    public class Person
    {
        private Telephone Telephone { get; set; }

        public void SetNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));
            if (!number.StartsWith("+98"))
                throw new ArgumentOutOfRangeException(nameof(number));

            Telephone.Number = number;
        }
    }

    public class Company
    {
        private Telephone Telephone { get; set; }
        public void SetNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));
            if (!number.StartsWith("+98"))
                throw new ArgumentOutOfRangeException(nameof(number));

            Telephone.Number = number;
        }
    }

    public class OrganizationBranch
    {
        private Telephone Telephone { get; set; }
        public void SetNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));
            if (!number.StartsWith("+98"))
                throw new ArgumentOutOfRangeException(nameof(number));

            Telephone.Number = number;
        }
    }
    public class Employee
    {
        private Telephone Telephone { get; set; }
        public void SetNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));
            if (!number.StartsWith("+98"))
                throw new ArgumentOutOfRangeException(nameof(number));

            Telephone.Number = number;
        }
    }
}