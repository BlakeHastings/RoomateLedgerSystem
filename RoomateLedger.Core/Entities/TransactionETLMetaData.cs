using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.Core.Entities
{
    public partial class TransactionETLMetaData
    {
        public DateTime LoadDateTime { get; set; }
        public string PipelineOriginId { get; set; }
        public string Source { get; set; }

        public string TransactionId { get; set; } 
        public Transaction Transaction { get; set; }
    }
}
