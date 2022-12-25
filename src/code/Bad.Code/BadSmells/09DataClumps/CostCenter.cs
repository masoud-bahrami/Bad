namespace Bad.Code.BadSmells._09DataClumps
{
    public class CostCenter
    {
        public string Id { get; set; }

        public string DetailLedgerCode { get; private set; }
        public string DetailLedgerTitle { get; private set; }
    }
}