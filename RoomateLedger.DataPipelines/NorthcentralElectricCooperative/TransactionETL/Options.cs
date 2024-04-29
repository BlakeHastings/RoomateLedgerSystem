using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL
{
    public class Options
    {
        /// <summary>
        /// 'Username' for online login. 
        /// You must have this OR <see cref="NorthcentralUserID"/> set.
        /// </summary>
        public string NorthcentralAccountNumber { get; set; } = String.Empty;
        /// <summary>
        /// 'Username' for online login. 
        /// You must have this OR <see cref="NorthcentralUserID"/> set.
        /// </summary>
        public string NorthcentralUserID { get; set; } = String.Empty;
        public string NorthcentralPassword { get; set; } = String.Empty;
    }
}
