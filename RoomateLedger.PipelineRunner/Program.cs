using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RoomateLedger.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using ProgressResidential = RoomateLedger.DataPipelines.ProgressResidential;
using RoomateLedger.DataPipelines.ProgressResidential.TransactionETL.Exporters.Selenium;
using RoomateLedger.PipelineRunner;
using RoomateLedger.DataPipelines.Core;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddOptions<ProgressResidential.TransactionETL.Options>()
    .Bind(builder.Configuration.GetSection("ProgressResidentialTransactionETLPipeline"));

builder.Services.AddDbContext<LedgerContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

builder.Services.AddTransient<IRecentActivityExporter, RecentActivityExporter>();
builder.Services.AddTransient<ProgressResidential.TransactionETL.Pipeline>();
builder.Services.AddTransient<ITransactionUploader, TransactionUploader>();

builder.Services.AddHostedService<PipelineRunner>();

var app = builder.Build();
await app.StartAsync();


