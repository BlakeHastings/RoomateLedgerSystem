using RoomateLedger.Core.Entities;
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Models;
using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Transformers
{
    public class AccountHistoryToTransactionMapper
    {

        public static IEnumerable<Transaction> Transform(IEnumerable<AccountHistory> accountHistories, string source, string pipelineOriginId)
        {
            return accountHistories.Select(x => Transform(x, source, pipelineOriginId));
        }

        public static Transaction Transform(AccountHistory accountHistory, string source, string pipelineOriginId)
        {
            var transaction = new Transaction()
            {
                //TDOO: Replace this with proper format/hash
                TransactionId = $"{source}{accountHistory.SourceSeqNo}{accountHistory.Amount}{accountHistory.Balance}",
                Amount = Decimal.Abs(Decimal.Parse(accountHistory.Amount)),
                DateTime = DateTime.Parse(accountHistory.SystemTime),
                Description = accountHistory.TransDesc,
                Source = source,
                ETLMetaData = new TransactionETLMetaData()
                {
                    LoadDateTime = DateTime.UtcNow,
                    PipelineOriginId = pipelineOriginId,
                    Source = source,
                }
            };

            // set type
            switch (accountHistory.TransType)
            {
                case "Payment":
                    transaction.Type = "Credit";
                    break;
                case "Bill":
                    transaction.Type = "Debit";
                    break;
                default:
                    // want to be aware if more transaction types are added
                    throw new Exception($"Unrecognized transaction type: {accountHistory.TransType}");
            }

            return transaction;
        }

    }
}
