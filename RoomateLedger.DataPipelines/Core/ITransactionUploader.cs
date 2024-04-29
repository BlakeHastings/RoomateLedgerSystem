using RoomateLedger.Core.Entities;

namespace RoomateLedger.DataPipelines.Core
{
    public interface ITransactionUploader
    {
        public Task UploadAsync(IEnumerable<Transaction> transactions);
    }
}