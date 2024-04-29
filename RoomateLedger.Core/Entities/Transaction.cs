namespace RoomateLedger.Core.Entities
{
    public partial class Transaction
    {
        public string TransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public TransactionETLMetaData ETLMetaData { get; set; }
    }
}