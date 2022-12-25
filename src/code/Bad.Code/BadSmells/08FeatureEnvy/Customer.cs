using System;

namespace Bad.Code.BadSmells._08FeatureEnvy
{
    // https://elearning.industriallogic.com/gh/submit?Action=PageAction&album=recognizingSmells&path=recognizingSmells/featureEnvy/featureEnvyExample&devLanguage=Java
    public class Phone
    {
        private readonly string _unformattedNumber;
        public Phone(string unformattedNumber)
        {
            this._unformattedNumber = unformattedNumber;
        }
        public string GetAreaCode()
        {
            return _unformattedNumber.Substring(0, 3);
        }
        public string GetPrefix()
        {
            return _unformattedNumber.Substring(3, 6);
        }
        public string GetNumber()
        {
            return _unformattedNumber.Substring(6, 10);
        }
    }

    public class Customer
    {
        private readonly Phone _mobilePhone;

        public Customer(Phone mobilePhone)
        {
            this._mobilePhone = mobilePhone;
        }

        public string GetMobilePhoneNumber()
        {
            return "(" +
                   "Area code is:" + _mobilePhone.GetAreaCode() + ") " + Environment.NewLine +
                   "Prefix is:" + _mobilePhone.GetPrefix() + "-" + Environment.NewLine +
                   "Number is:" + _mobilePhone.GetNumber()
                   + Environment.NewLine + "and Full number is :" + _mobilePhone.GetAreaCode() + _mobilePhone.GetPrefix() + _mobilePhone.GetNumber();
        }
    }
}