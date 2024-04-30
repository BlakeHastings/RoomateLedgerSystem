using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Exporters.Selenium
{
    public record AccountTransaction
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public string Date { get; set; }
        public string OriginationDate { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public bool ImagesExist { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string OfxId { get; set; }
        public string DepositNumber { get; set; }
    }
}
