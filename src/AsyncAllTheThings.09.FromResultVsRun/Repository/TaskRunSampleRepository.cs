using System.Threading.Tasks;

namespace AsyncAllTheThings._09.FromResultVsRun.Repository
{
    public class TaskRunSampleRepository : ISampleRepository
    {
        private readonly NonAsyncOrmContext _context;

        public TaskRunSampleRepository(NonAsyncOrmContext context)
        {
            _context = context;
        }

        public async Task<SampleEntity> GetByIdAsync(int id)
        {
            return await Task.Run(() => _context.Samples.Find(id));
        }
    }
}
