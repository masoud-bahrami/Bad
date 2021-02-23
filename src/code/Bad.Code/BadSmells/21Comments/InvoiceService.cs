using System;
using System.Collections.Generic;
using Bad.Code._05MutableData;

namespace Bad.Code._21Comments
{
    public class InvoiceService
    {
        public void ApplyDiscount(int invoiceId, Discount discount)
        {
            Invoice invoice = RetrieveInvoice(invoiceId);

            //Check if discount amount is valid.
            //discount amount should be greater than zero
            if (discount.Amount < 0)
                throw new Exception();

            //...
        }

        private Invoice RetrieveInvoice(int invoiceId)
        {
            return new Invoice();
        }
    }

    public class Invoice
    {
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
    }
}