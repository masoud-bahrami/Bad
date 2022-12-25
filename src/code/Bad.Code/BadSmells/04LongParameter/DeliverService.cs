using System;
using System.Collections.Generic;

namespace Bad.Code.BadSmells._04LongParameter
{
    public class DeliverService
    {
        public DateTime GetDeliveryDate(string deliveryState , bool isRush)
        {
            DateTime result;

            if (isRush)
            {
                if (new List<string> { "THR", "KHR" }.Contains(deliveryState))
                    result = DateTime.Now.AddDays(5);
                else if (new List<string> { "QM", "ARK" }.Contains(deliveryState))
                    result = DateTime.Now.AddDays(7);
                else result = DateTime.Now.AddDays(6);
            }
            else
            {
                if (new List<string> { "THR", "KHR" }.Contains(deliveryState))
                    result = DateTime.Now.AddDays(1);
                else if (new List<string> { "KHZ", "ESF" }.Contains(deliveryState))
                    result = DateTime.Now.AddDays(2);
                else result = DateTime.Now.AddDays(3);
            }
            
            return result;
        }
    }

    public class Order
    {
        public string DeliveryState { get; set; }
        public bool IsRush { get; set; }
    }
}