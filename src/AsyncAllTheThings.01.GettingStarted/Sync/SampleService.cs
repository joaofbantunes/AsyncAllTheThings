namespace AsyncAllTheThings._01.GettingStarted.Sync
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _sampleRepository;

        public SampleService(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public SampleEntity GetSampleById(long id)
        {
            var sample = _sampleRepository.GetById(id);
            return sample;
        }

        public void UpsertSample(SampleEntity sample)
        {
            var existingSample = _sampleRepository.GetById(sample.Id);

            if (existingSample != null)
            {
                existingSample.Text = sample.Text;
                _sampleRepository.Update(existingSample);
            }
            else
            {
                _sampleRepository.Add(sample);
            }
        }
    }
}
