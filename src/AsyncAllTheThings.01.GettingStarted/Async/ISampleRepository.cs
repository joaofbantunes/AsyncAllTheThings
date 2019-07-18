using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncAllTheThings._01.GettingStarted.Async
{
    public interface ISampleRepository
    {
        Task<SampleEntity> GetByIdAsync(long id);

        Task<IReadOnlyCollection<SampleEntity>> GetAllAsync();

        Task<SampleEntity> AddAsync(SampleEntity sample);

        Task<SampleEntity> UpdateAsync(SampleEntity sample);

        Task DeleteAsync(SampleEntity sample);
    }
}
