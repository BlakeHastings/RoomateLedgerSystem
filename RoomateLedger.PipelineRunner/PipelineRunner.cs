using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProgressResidential = RoomateLedger.DataPipelines.ProgressResidential;
using NorthcentralElectric = RoomateLedger.DataPipelines.NorthcentralElectricCooperative;
using RoomateLedger.Infrastructure.Database;
using RoomateLedger.DataPipelines.Common;

namespace RoomateLedger.PipelineRunner;

public class PipelineRunner : IHostedService
{
    private ILogger<PipelineRunner> _logger { get; set; }
    private LedgerContext _dbContext { get; }
    private IEnumerable<BasePipeline> _pipelines;

    public PipelineRunner(
        ILogger<PipelineRunner> logger,
        LedgerContext dbContext,
        IEnumerable<BasePipeline> pipelines) { 
        _logger = logger;
        _dbContext = dbContext;
        _pipelines = pipelines;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger?.LogInformation("Ensuring database creation");
        await _dbContext.Database.EnsureCreatedAsync();

        foreach (var pipeline in _pipelines)
        {
            _logger?.LogInformation($"Running {pipeline.PipelineName}");
            try
            {
                await pipeline.ExecuteAsync();
            }catch(Exception ex)
            {
                _logger?.LogError(ex.Message);
            }
            
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
