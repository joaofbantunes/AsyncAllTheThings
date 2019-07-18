using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AsyncAllTheThings._14.DuckTyping.Ducks
{
    public class SampleAwaiter<T> : INotifyCompletion
    {
        private TaskAwaiter<T> _awaiter;

        public bool IsCompleted => _awaiter.IsCompleted;

        internal SampleAwaiter(Task<T> task) => _awaiter = task.GetAwaiter();

        public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);

        public T GetResult() => _awaiter.GetResult();
    }
}
