using RoomateLedger.Core.Entities;
using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Transformers
{
    public class RecentActivityToTransactionMapper
    {

        public static Transaction Transform(RecentActivity recentActivity, string source, string pipelineOriginId)
        {
            // this shouldn't ever happen but now there is awareness if it does
            if (recentActivity.Charge != 0 && recentActivity.Payment != 0)
                throw new Exception("Activity has value for charge and payment. Logic cannot handle this.");

            return new Transaction()
            {
                //TDOO: Replace this with proper format/hash
                TransactionId = $"{source}{recentActivity.Date}{recentActivity.Balance}",
                Amount = (recentActivity.Charge != 0) ? recentActivity.Charge : recentActivity.Payment,
                Type = (recentActivity.Charge != 0) ? "Debit" : "Credit",
                DateTime = recentActivity.Date.ToDateTime(TimeOnly.MinValue),
                Description = recentActivity.PaymentsAndCharges,
                Source = source,
                ETLMetaData = new TransactionETLMetaData()
                {
                    LoadDateTime = DateTime.UtcNow,
                    PipelineOriginId = pipelineOriginId,
                    Source = source,
                }
            };
        }

    }
}
