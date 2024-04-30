using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.FirstHorizon.TransactionETL
{
    public class Options
    {
        public string FirstHorizonAccountNumber { get; set; } = String.Empty;
        public string FirstHorizonUserID { get; set; } = String.Empty;
        public string FirstHorizonPassword { get; set; } = String.Empty;
    }
}
