namespace RoomateLedger.Core.Entities
{
    public partial class Transaction
    {
        public string TransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        /// <summary>
        /// Describes the context of the transaction. Credit or Debit
        /// </summary>
        public string Type { get; set; }

        public TransactionETLMetaData ETLMetaData { get; set; }
    }
}