using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RoomateLedger.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using ProgressResidential = RoomateLedger.DataPipelines.ProgressResidential;
using NorthcentralElectric = RoomateLedger.DataPipelines.NorthcentralElectricCooperative;
using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Exporters.Selenium;
using RoomateLedger.PipelineRunner;
using RoomateLedger.DataPipelines.Core;
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Exporters.Selenium;
using RoomateLedger.DataPipelines.Common;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

//TODO: Add extension methods inside pipeline libraries for adding services and options
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddOptions<ProgressResidential.TransactionETL.Options>()
    .Bind(builder.Configuration.GetSection("ProgressResidentialTransactionETLPipeline"));

builder.Services.AddOptions<NorthcentralElectric.TransactionETL.Options>()
    .Bind(builder.Configuration.GetSection("NorthcentralElectricTransactionETLPipeline"));

builder.Services.AddDbContext<LedgerContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

// pipeline referenced services
builder.Services.AddTransient<IRecentActivityExporter, RecentActivityExporter>();
builder.Services.AddTransient<IAccountHistoryExporter, AccountHistoryExporter>();
builder.Services.AddTransient<ITransactionUploader, TransactionUploader>();

// pipeline services
builder.Services.AddTransient<BasePipeline, ProgressResidential.TransactionETL.Pipeline>();
builder.Services.AddTransient<BasePipeline, NorthcentralElectric.TransactionETL.Pipeline>();

builder.Services.AddHostedService<PipelineRunner>();

var app = builder.Build();
await app.StartAsync();


