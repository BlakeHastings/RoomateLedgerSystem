using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Models;

namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Exporters.Selenium
{
    public interface IAccountHistoryExporter
    {
        public IEnumerable<AccountHistory> Export(string username, string password);
    }
}