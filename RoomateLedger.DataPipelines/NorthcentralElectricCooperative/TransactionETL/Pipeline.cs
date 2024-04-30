using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoomateLedger.DataPipelines.Common;
using RoomateLedger.DataPipelines.Core;
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Exporters.Selenium;
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL
{
    public class Pipeline : BasePipeline
    {
        private readonly Options _options;
        private readonly ILogger? _logger;
        private readonly IAccountHistoryExporter _accountHistoryExporter;
        private readonly ITransactionUploader _transactionUploader;
        private const string DATASOURCE = "NorthCentralElectricCooperative";

        public Pipeline(
            IOptions<Options> options,
            IAccountHistoryExporter accountHistoryExporter,
            ITransactionUploader transactionUploader,
            ILogger<Pipeline>? logger)
        {
            // TODO: fill this in with correct information
            this.PipelineDescription = "WILL FILL THIS IN LATER";
            this.PipelineName = $"{DATASOURCE}.TransactionETL";
            this.PipelineId = $"{DATASOURCE}.TransactionETL";

            _options = options.Value;
            _logger = logger;
            _accountHistoryExporter = accountHistoryExporter;
            _transactionUploader = transactionUploader;
        }

        public override async Task ExecuteAsync(CancellationToken? cancellationToken = null)
        {
            _logger?.LogInformation("");

            // retrieve source information
            var accountHistory = _accountHistoryExporter.Export(_options.NorthcentralAccountNumber ?? _options.NorthcentralUserID, _options.NorthcentralPassword);

            // map source formated data to transaction format
            accountHistory      = AccountHistoryPruner.Transform(accountHistory);
            var transactions    = AccountHistoryToTransactionMapper.Transform(accountHistory, DATASOURCE, this.PipelineId);

            // upload transactions to database
            await _transactionUploader.UploadAsync(transactions);
        }
    }
}
