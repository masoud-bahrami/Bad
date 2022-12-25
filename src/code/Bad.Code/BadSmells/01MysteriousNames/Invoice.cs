namespace Bad.Code.BadSmells._01MysteriousNames
{
    public class Invoice
    {
        public int itemsCnt { get; set; }
        public int GetInvoicecount()
        {
            return itemsCnt;
        }        
    }
}