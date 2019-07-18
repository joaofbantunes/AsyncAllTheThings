namespace AsyncAllTheThings._01.GettingStarted.Sync
{
    public interface ISampleService
    {
        SampleEntity GetSampleById(long id);

        void UpsertSample(SampleEntity sample);
    }
}
