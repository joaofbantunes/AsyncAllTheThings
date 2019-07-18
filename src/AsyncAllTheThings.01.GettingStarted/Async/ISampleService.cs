using System.Threading.Tasks;

namespace AsyncAllTheThings._01.GettingStarted.Async
{
    public interface ISampleService
    {
        Task<SampleEntity> GetSampleByIdAsync(long id);

        Task UpsertSampleAsync(SampleEntity sample);
    }
}
