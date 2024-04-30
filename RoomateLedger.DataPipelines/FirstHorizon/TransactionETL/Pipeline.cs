using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RoomateLedger.DataPipelines.Common;
using RoomateLedger.DataPipelines.Core;
using RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Exporters.Selenium;
using RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.FirstHorizon.TransactionETL
{
    public class Pipeline : BasePipeline
    {
        private Options _options;
        private ILogger? _logger;
        private IAccountTransactionExporter _accountTransactionExporter;
        private ITransactionUploader _transactionUploader;

        private const string DATASOURCE = "FirstHorizon";

        public Pipeline(IOptions<Options> options, ILogger? logger, IAccountTransactionExporter accountTransactionExporter, ITransactionUploader transactionUploader) 
            : base($"{DATASOURCE}.TransactionETL001", $"{DATASOURCE}.TransactionETL001", "FILL THIS IN LATER")
        {
            _options = options.Value;
            _logger = logger;
            _accountTransactionExporter = accountTransactionExporter;
            _transactionUploader = transactionUploader;
        }

        public override async Task ExecuteAsync(CancellationToken? cancellationToken = null)
        {
            _logger?.LogInformation("");

            // retrieve source formated data
            var accountTransactions = _accountTransactionExporter.Export(_options.FirstHorizonAccountNumber, _options.FirstHorizonUserID, _options.FirstHorizonPassword);

            // transform data
            //var transactions = AccountTransactionToTransactionMapper.Transform(accountTransactions, DATASOURCE,)

            // load data
            //await _transactionUploader.UploadAsync()

            _logger?.LogInformation("Transaction data gathered");
        }
    }
}
