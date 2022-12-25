using System;
using System.Collections.Generic;

namespace Bad.Code.BadSmells._03LongFunction
{
    public class DateInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string CardNumber { get; set; }
    }
    public class CacheBackService
    {
        public void Calculate(DateInterval date,
                                List<Transaction> transactions,
                                string firstName,
                                string lastName)
        {
            Console.WriteLine($"**** Welcome {firstName} {lastName} *****");
            Console.WriteLine("**** BigCompany ***");

            double ch = 0;

            if (!(DateTime.Now < date.Start) && !(DateTime.Now > date.End))
                ch = transactions.Count * .009;
            else
                ch = transactions.Count * .0009;

            decimal chBack = 0;

            string result = "";

            foreach (var transaction in transactions)
            {

                decimal cacheBack = 0;

                switch (transaction.CardType)
                {
                    case CardType.GoldCard:
                        if (transaction.TransactionType == TransactionType.Hotel)
                        {
                            cacheBack = 25000;
                        }
                        else if (transaction.TransactionType == TransactionType.Restaurant)
                        {
                            cacheBack = 15000;
                        }
                        else if (transaction.TransactionType == TransactionType.CarRental)
                        {
                            cacheBack = 35000;
                        }

                        break;
                    case CardType.SilverCard:
                        if (transaction.TransactionType == TransactionType.Hotel)
                        {
                            cacheBack = 20000;
                        }
                        else if (transaction.TransactionType == TransactionType.Restaurant)
                        {
                            cacheBack = 10000;
                        }
                        else if (transaction.TransactionType == TransactionType.CarRental)
                        {
                            cacheBack = 30000;
                        }

                        break;
                    case CardType.BronzeCard:
                        if (transaction.TransactionType == TransactionType.Hotel)
                        {
                            cacheBack = 15000;
                        }
                        else if (transaction.TransactionType == TransactionType.Restaurant)
                        {
                            cacheBack = 9500;
                        }
                        else if (transaction.TransactionType == TransactionType.CarRental)
                        {
                            cacheBack = 27500;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                chBack += cacheBack;

                result += $"{transaction.CardType} - {transaction.TransactionType} - {transaction.Amount}. Your earned {cacheBack} Toman \n";
            }

            chBack += Math.Ceiling((chBack * (decimal)ch)) / 100;

            result += $"totalCacheBack you earned {chBack}";

            Console.WriteLine(result);

            Console.WriteLine("**** Take care yourself****");
            Console.WriteLine("**** GoodBy **********");
        }

        private DateTime CuurentDate()
        {
            return DateTime.Now;
        }
    }

    public abstract class Transaction
    {
        public CardType CardType { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }

    public class GoldCardTransaction : Transaction
    {
        
    }

    public class BronzeCardTransaction : Transaction
    {
    }

    public enum TransactionType
    {
        Hotel,
        Restaurant,
        CarRental
    }

    public enum CardType
    {
        GoldCard,
        SilverCard,
        BronzeCard
    }
}