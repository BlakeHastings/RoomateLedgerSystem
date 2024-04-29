using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Models;

namespace RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Exporters.Selenium
{
    public interface IRecentActivityExporter
    {
        public IEnumerable<RecentActivity> Export(string subdomain, string username, string password);
    }
}
