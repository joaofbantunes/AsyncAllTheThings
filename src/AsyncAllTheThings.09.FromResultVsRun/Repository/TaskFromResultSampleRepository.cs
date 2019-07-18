using System.Threading.Tasks;

namespace AsyncAllTheThings._09.FromResultVsRun.Repository
{
    public class TaskFromResultSampleRepository : ISampleRepository
    {
        private readonly NonAsyncOrmContext _context;

        public TaskFromResultSampleRepository(NonAsyncOrmContext context)
        {
            _context = context;
        }

        public Task<SampleEntity> GetByIdAsync(int id)
        {
            return Task.FromResult(_context.Samples.Find(id));
        }
    }
}
