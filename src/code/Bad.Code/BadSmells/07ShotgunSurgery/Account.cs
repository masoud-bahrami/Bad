using System;

namespace Bad.Code.BadSmells._07ShotgunSurgery
{
    //https://dzone.com/articles/code-smell-shot-surgery
    public class Account
    {
        private string _type;
        private string _accountNumber;
        private int _amount;

        public Account(string type, string accountNumber, int amount)
        {
            this._amount = amount;
            this._type = type;
            this._accountNumber = accountNumber;
        }


        public void Debit(int debit)
        {
            if (_amount <= 500)
            {
                Console.WriteLine("amount should be over 500");
                throw new Exception("Minimum balance should be over 500");
            }

            _amount -= debit;
            Console.WriteLine("Now, the amount is" + _amount);
        }

        public void Transfer(Account from, int creditAmount)
        {
            if (from._amount <= 500)
            {
                throw new Exception("Minimum balance should be over 500");
            }

            from.Debit(creditAmount);
            _amount = _amount + creditAmount;
        }
        
    }
}