using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Transformers
{
    public class AccountHistoryPruner
    {

        public static IEnumerable<AccountHistory> Transform(IEnumerable<AccountHistory> accountHistories)
        {
            // NorthCentral has some ledger items that don't change the balance which we don't care about
            return accountHistories.Where(x => x.BalUpdate != "N"); 
        }
    }
}
