using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RoomateLedger.DataPipelines.Core;
using RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Exporters.Selenium;
using RoomateLedger.DataPipelines.Common;

namespace RoomateLedger.DataPipelines.NorthcentralElectricCooperative.TransactionETL.Extensions
{
    public static class HostBuilderExtensions
    {

        public static IHostApplicationBuilder AddNorthcentralTransactionETL(this IHostApplicationBuilder builder, string sectionName)
        {
            builder.Services.AddOptions<Options>()
                .Bind(builder.Configuration.GetSection(sectionName));
            
            builder.Services.AddTransient<IAccountHistoryExporter, AccountHistoryExporter>();
            builder.Services.AddTransient<ITransactionUploader, TransactionUploader>();

            builder.Services.AddTransient<BasePipeline, Pipeline>();

            return builder;
        }

    }
}
