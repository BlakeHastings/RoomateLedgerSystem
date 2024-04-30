namespace RoomateLedger.DataPipelines.Common
{
    public abstract class BasePipeline
    {
        /// <summary>
        /// A pipeline relative unique ID
        /// </summary>
        public string PipelineId { get; protected set; } = "BasePipeline";
        public string PipelineName { get; protected set; } = "BasePipeline";
        public string PipelineDescription { get; protected set; } = "The root of all pipelines";

        protected BasePipeline(string pipelineId, string pipelineName, string pipelineDescription)
        {
            PipelineId = pipelineId;
            PipelineName = pipelineName;
            PipelineDescription = pipelineDescription;
        }

        public abstract Task ExecuteAsync(CancellationToken? cancellationToken = null);
    }
}