using System;

namespace Bad.Code.BadSmells._09DataClumps
{
    public class Visa
    {
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }

        public void SetPrice(decimal amount, string currency)
        {
            PriceAmount = amount;
            PriceCurrency = currency;
        }

        public decimal GetPriceToRial()
        {
            if(PriceCurrency == "Rial")
                return PriceAmount;
            if (PriceCurrency == "Toman")
                return PriceAmount * 10;

            throw new ArgumentOutOfRangeException("PriceCurrency");

        }
    }

    public class TourismHealthService
    {
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }

        public void SetPrice(decimal amount, string currency)
        {
            PriceAmount = amount;
            PriceCurrency = currency;
        }

        public decimal GetPriceToRial()
        {
            if (PriceCurrency == "Rial")
                return PriceAmount;
            if (PriceCurrency == "Toman")
                return PriceAmount * 10;

            throw new ArgumentOutOfRangeException("PriceCurrency");
        }

    }
}
