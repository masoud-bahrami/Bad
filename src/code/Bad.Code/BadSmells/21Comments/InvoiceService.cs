using System;
using System.Collections.Generic;
using Bad.Code.BadSmells._05MutableData;

namespace Bad.Code.BadSmells._21Comments
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
        // orders in an invoice
        public List<Order> Orders { get; set; }
    }

    /// <summary>
    /// Created by Micheal!
    /// Last edited by Alex on 1998-02-03 20:30:00 
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
    }
}