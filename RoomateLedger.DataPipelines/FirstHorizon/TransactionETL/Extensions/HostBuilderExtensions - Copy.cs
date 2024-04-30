using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RoomateLedger.DataPipelines.Core;
using RoomateLedger.DataPipelines.Common;
using RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Exporters.Selenium;

namespace RoomateLedger.DataPipelines.FirstHorizon.TransactionETL.Extensions
{
    public static class HostBuilderExtensions
    {

        public static IHostApplicationBuilder AddFirstHorizonTransactionETL(this IHostApplicationBuilder builder, string sectionName)
        {
            builder.Services.AddOptions<Options>()
                .Bind(builder.Configuration.GetSection(sectionName));

            builder.Services.AddTransient<IAccountTransactionExporter, AccountTransactionExporter>();
            builder.Services.AddTransient<ITransactionUploader, TransactionUploader>();

            builder.Services.AddTransient<BasePipeline, Pipeline>();

            return builder;
        }

    }
}
