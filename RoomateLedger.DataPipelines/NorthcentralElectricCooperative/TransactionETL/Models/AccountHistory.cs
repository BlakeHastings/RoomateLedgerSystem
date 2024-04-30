namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Models
{
    public record AccountHistory
    {
        public string Id { get; set; }
        public string SourceSeqNo { get; set; }
        public string SystemTime { get; set; }
        public string TransDate { get; set; }
        public string Reference { get; set; }
        public string Batch { get; set; }
        public string Rate { get; set; }
        public string BalUpdate { get; set; }
        public string TransType { get; set; }
        public string TransDesc { get; set; }
        public string DelNotFlag { get; set; }
        public string Amount { get; set; }
        public string Arrears { get; set; }
        public string BalDiff { get; set; }
        public string Balance { get; set; }
    }
}
