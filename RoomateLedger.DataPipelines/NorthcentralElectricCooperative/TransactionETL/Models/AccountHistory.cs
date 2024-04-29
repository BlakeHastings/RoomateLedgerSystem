namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Models
{
    public record AccountHistory
    {
        public string Type { get; set; } = "Unknown";
        public DateTime SystemDateTime { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
