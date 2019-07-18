namespace AsyncAllTheThings._09.FromResultVsRun
{
    public class NonAsyncOrmContext
    {
        public IOrmSet<SampleEntity, int> Samples { get; }

        public interface IOrmSet<TEntity, TId>
        {
            TEntity Find(TId id);
        }
    }
}
