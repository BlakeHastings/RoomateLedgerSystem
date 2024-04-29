using Microsoft.EntityFrameworkCore;
using RoomateLedger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomateLedger.Infrastructure.Database
{
    public class LedgerContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionETLMetaData> ETLMetaData { get; set; }

        public LedgerContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasKey(e => e.TransactionId);

            modelBuilder.Entity<TransactionETLMetaData>()
                .HasOne(e => e.Transaction)
                .WithOne(e => e.ETLMetaData);

            modelBuilder.Entity<TransactionETLMetaData>()
                .HasKey(e => e.TransactionId);
        }
    }
}
