using CommandLine;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using RoomateLedger.Core.Entities;
using ProgressResidential = RoomateLedger.DataPipelines.ProgressResidential;
using RoomateLedger.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using RoomateLedger.DataPipelines.Common;

namespace RoomateLedger.PipelineRunner
{
    public class PipelineRunner : IHostedService
    {
        private ILogger<PipelineRunner> _logger { get; set; }
        private LedgerContext _dbContext { get; }
        private List<BasePipeline> pipelines = new List<BasePipeline>();

        public PipelineRunner(ILogger<PipelineRunner> logger, LedgerContext dbContext, ProgressResidential.TransactionETL.Pipeline pipeline) { 
            _logger = logger;
            _dbContext = dbContext;

            pipelines.Add(pipeline);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger?.LogInformation("Ensuring database creation");
            await _dbContext.Database.EnsureCreatedAsync();

            foreach (var pipeline in pipelines)
            {
                _logger?.LogInformation($"Running {pipeline.PipelineName}");
                await pipeline.ExecuteAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
