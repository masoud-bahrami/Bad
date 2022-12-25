using System;

namespace Bad.Code.BadSmells._10Primitive_Obsession
{
    // https://blog.ndepend.com/code-smell-primitive-obsession-and-refactoring-recipes/
    public class Account
    {
        public string CustomerName { get; private set; }
        public int AccountNumber { get; set; }
        public string Email { get; private set; }
        public string Address { get; set; }
    }

    public class CheckingAccount
    {
        public string CustomerName { get; private set; }
        public int AccountNumber { get; set; }
        public string Email { get; private set; }

        public string Address { get; set; }

        public string SocialSecurityNumber { get; set; }
        public DateTime ActiveDate { get; set; }
        
        
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string GetSSNLast4Digit()
        {
            int index = SocialSecurityNumber.LastIndexOf("-", StringComparison.Ordinal);
            return index > 0 && index < SocialSecurityNumber.Length
                ? SocialSecurityNumber.Substring(index + 1, SocialSecurityNumber.Length - index + 1)
                : SocialSecurityNumber;
        }
    }
}