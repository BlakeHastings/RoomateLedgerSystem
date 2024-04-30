using Microsoft.Extensions.Logging;
using RoomateLedger.DataPipelines.Common;
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
        

        public override async Task ExecuteAsync(CancellationToken? cancellationToken = null)
        {
            _logger?.LogInformation("");

            // retrieve source formated data


            _logger?.LogInformation("Transaction data gathered");
        }
    }
}
