using System.Collections.Generic;

namespace AsyncAllTheThings._01.GettingStarted.Sync
{
    public interface ISampleRepository
    {
        SampleEntity GetById(long id);

        IReadOnlyCollection<SampleEntity> GetAll();

        SampleEntity Add(SampleEntity sample);

        SampleEntity Update(SampleEntity sample);

        void Delete(SampleEntity sample);
    }
}
