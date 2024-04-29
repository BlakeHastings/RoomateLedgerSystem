using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Models
{
    /// <summary>
    /// Models data from the Recent Activity Table
    /// </summary>
    public record RecentActivity
    {
        public DateOnly Date;
        public string PaymentsAndCharges = "No Data";
        public decimal Charge;
        public decimal Payment;
        public decimal Balance;
    }
}
