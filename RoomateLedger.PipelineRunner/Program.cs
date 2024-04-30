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
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Extensions;
using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Extensions;
using RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddDbContext<LedgerContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

//builder.AddNorthcentralTransactionETL("NorthcentralElectricTransactionETLPipeline");
//builder.AddProgressResidentialTransactionETL("ProgressResidentialTransactionETLPipeline");
builder.AddFirstHorizonTransactionETL("FirstHorizonTransactionETLPipeline");

builder.Services.AddHostedService<PipelineRunner>();

var app = builder.Build();
await app.StartAsync();


