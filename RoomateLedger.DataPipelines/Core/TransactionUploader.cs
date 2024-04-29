using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using RoomateLedger.Core.Entities;
using RoomateLedger.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.DataPipelines.Core
{
    // TODO: Replace this with a proper Repo pattern later. This is good enough for right now
    public class TransactionUploader : ITransactionUploader
    {
        private LedgerContext _dbContext;

        public TransactionUploader(LedgerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UploadAsync(IEnumerable<Transaction> transactions)
        {
            _dbContext.Transactions.AddRange(transactions);
            await _dbContext.SaveChangesAsync();
        }

    }
}
