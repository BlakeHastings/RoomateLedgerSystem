using RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Models;

namespace RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Exporters.Selenium
{
    public interface IAccountTransactionExporter
    {
        IEnumerable<AccountTransaction> Export(string accountNumber, string username, string password);
    }
}