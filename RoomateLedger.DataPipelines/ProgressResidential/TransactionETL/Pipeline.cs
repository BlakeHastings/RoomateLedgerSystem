using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoomateLedger.DataPipelines.Common;
using RoomateLedger.DataPipelines.Core;
using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Exporters.Selenium;
using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Transformers;

namespace RoomateLedger.DataPipelines.ProgressResidential.TransactionETL
{
    public class Pipeline : BasePipeline
    {
        private Options _options;
        private IRecentActivityExporter _recentActivityExporter;
        private ITransactionUploader _transactionUploader;
        private ILogger? _logger;

        private const string RENTCAFE_SUBDOMAIN = "rentprogress";
        private const string DATASOURCE = "ProgressResidential";

        public Pipeline(IOptions<Options> options, IRecentActivityExporter recentActivityExporter, ITransactionUploader transactionUploader,  ILogger<Pipeline> logger = null)
        {
            this.PipelineDescription = "Extracts Recent Activity information from Progress Residentail's secure cafe page, transforms it into the Transactions model, and uploads it to the database";
            this.PipelineName = "ProgressResidential.TransactionETL";
            this.PipelineId = "ProgressResidential.TransactionETL";

            _options = options.Value;
            _recentActivityExporter = recentActivityExporter;
            _transactionUploader = transactionUploader;
        }

        // TODO: Implement CancellationToken fully
        public override async Task ExecuteAsync(CancellationToken? cancellationToken = null)
        {
            _logger?.LogInformation("");

            // retrieve source formated data
            var recentActivity = _recentActivityExporter.Export(RENTCAFE_SUBDOMAIN, _options.SecureCafeEmail, _options.SecureCafePassword);

            // map source formated data to transaction format
            var transactions = recentActivity.Select(activity => RecentActivityToTransactionMapper.Transform(activity, DATASOURCE, PipelineId));

            // upload transactions to database
            await _transactionUploader.UploadAsync(transactions);

            _logger?.LogInformation("Transaction data gathered");
        }
    }
}
