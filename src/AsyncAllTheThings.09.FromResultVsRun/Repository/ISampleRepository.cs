using System.Threading.Tasks;

namespace AsyncAllTheThings._09.FromResultVsRun.Repository
{
    public interface ISampleRepository
    {
        Task<SampleEntity> GetByIdAsync(int id);

        // more methods...
    }
}
