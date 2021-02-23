using System;
using System.Collections.Generic;
using System.Linq;

namespace Bad.Code.Performances
{
    public class Invoice
    {

        private readonly string _customer;
        private IList<Play> _plays { get; set; }
        public string Customer { get; private set; }
        public List<Performance> Performances { get; private set; }

        public Invoice(IList<Play> plays, List<Performance> performances, string customer)
        {
            _plays = plays;
            Performances = performances;
            _customer = customer;
        }
        public string Statement()
        {
            decimal totalAmount = 0;
            decimal volumeCredits = 0;

            string result = $"Statement for {Customer}\n";

            foreach (var perf in Performances)
            {
                var play = _plays.FirstOrDefault(p => p.PlayId == perf.PlayId);
                decimal thisAmount = 0;
                switch (play.Type)
                {
                    case PlayType.Tragedy:
                        thisAmount = 40000;
                        if (perf.Audience > 30)
                            thisAmount += 1000 * (perf.Audience - 30);
                        break;

                    case PlayType.Comedy:
                        thisAmount = 30000;
                        if (perf.Audience > 20)
                            thisAmount += 1000 + 500 * (perf.Audience - 20);
                        thisAmount += 300 * perf.Audience;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                volumeCredits += Math.Max(perf.Audience - 30, 0);

                if (PlayType.Comedy == play.Type)
                    volumeCredits += (perf.Audience / 10) * 1000;

                result += $"{play.Name}: {thisAmount / 100} {perf.Audience}";
                thisAmount += thisAmount;
            }

            result += $"Amount owed is {totalAmount / 100}\n";
            result += $"Yor earned {volumeCredits} credits\n";

            return result;
        }
    }
}