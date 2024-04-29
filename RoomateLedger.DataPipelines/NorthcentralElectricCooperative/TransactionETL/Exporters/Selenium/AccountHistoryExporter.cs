﻿using Microsoft.Extensions.Logging;
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Exporters.Selenium
{
    public class AccountHistoryExporter : IAccountHistoryExporter
    {

        private ILogger<AccountHistoryExporter>? _logger { get; set; }

        public AccountHistoryExporter(ILogger<AccountHistoryExporter>? logger = null)
        {
            _logger = logger;
        }
        
        public IEnumerable<AccountHistory> Export(string username, string password)
        {
            throw new NotImplementedException();
        }
    

    }
}
