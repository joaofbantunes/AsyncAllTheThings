using System.Threading.Tasks;

namespace AsyncAllTheThings._01.GettingStarted.Async
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _sampleRepository;

        public SampleService(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task<SampleEntity> GetSampleByIdAsync(long id)
        {
            var sample = await _sampleRepository.GetByIdAsync(id);
            return sample;
        }

        public async Task UpsertSampleAsync(SampleEntity sample)
        {
            var existingSample = await _sampleRepository.GetByIdAsync(sample.Id);

            if (existingSample != null)
            {
                existingSample.Text = sample.Text;
                await _sampleRepository.UpdateAsync(existingSample);
            }
            else
            {
                await _sampleRepository.AddAsync(sample);
            }
        }
    }
}
