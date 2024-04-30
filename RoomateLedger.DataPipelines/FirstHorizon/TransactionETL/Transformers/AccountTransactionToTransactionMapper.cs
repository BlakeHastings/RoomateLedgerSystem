using RoomateLedger.Core.Entities;
using RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Transformers
{
    public class AccountTransactionToTransactionMapper
    {

       public static Transaction Transform(AccountTransaction accountTransaction, string source, string pipelineOriginId)
        {
            //// this shouldn't ever happen but now there is awareness if it does
            //if (accountTransaction.Amount != 0 && accountTransaction.Balance != 0)
            //    throw new Exception("Activity has value for charge and payment. Logic cannot handle this.");
            //return new Transaction()
            //{
            //    //TDOO: Replace this with proper format/hash
            //    TransactionId = $"{source}{accountTransaction.Date}{accountTransaction.Balance}",
            //    Amount = (accountTransaction.Amount != 0) ? accountTransaction.Amount : accountTransaction.Balance,
            //    Type = (accountTransaction.Amount != 0) ? "Debit" : "Credit",
            //    DateTime = accountTransaction.Date.ToDateTime(TimeOnly.MinValue),
            //    Description = accountTransaction.Name,
            //    Source = source,
            //    ETLMetaData = new TransactionETLMetaData()
            //    {
            //        LoadDateTime = DateTime.UtcNow,
            //        PipelineOriginId = pipelineOriginId,
            //        Source = source,
            //    }
            //};

            throw new NotImplementedException();
        }

    }
}
