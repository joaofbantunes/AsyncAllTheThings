using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAllTheThings._04.WastingThreads.Controllers
{
    public class SampleController : ControllerBase
    {
        private readonly List<int> _handlingThreads = new List<int>();

        [HttpGet("when-we-block-a-thread")]
        public ActionResult<string> WhenWeBlockAThread()
        {
            _handlingThreads.Add(Thread.CurrentThread.ManagedThreadId);
            DoSomethingAsync().GetAwaiter().GetResult();
            _handlingThreads.Add(Thread.CurrentThread.ManagedThreadId);

            var builder = new StringBuilder();
            builder.AppendLine($"Handling starts on thread {_handlingThreads[0]}");
            builder.AppendLine($"The async method starts on the same thread as it was called, so {_handlingThreads[1]}");
            builder.AppendLine($"But continues on a new one, so {_handlingThreads[2]}");
            builder.AppendLine($"Finally, the calling method resumes on the original thread, as it was blocked {_handlingThreads[3]}");
            return builder.ToString();
        }

        [HttpGet("when-we-dont-block-a-thread")]
        public async Task<ActionResult<string>> WhenWeDontBlockAThreadAsync()
        {
            _handlingThreads.Add(Thread.CurrentThread.ManagedThreadId);
            await DoSomethingAsync();
            _handlingThreads.Add(Thread.CurrentThread.ManagedThreadId);

            var builder = new StringBuilder();
            builder.AppendLine($"Handling starts on thread {_handlingThreads[0]}");
            builder.AppendLine($"The async method starts on the same thread as it was called, so {_handlingThreads[1]}");
            builder.AppendLine($"Then may continue on any thread, it depends on the threads available in the thread pool. In this case it was {_handlingThreads[2]}");
            builder.AppendLine($"Finally, the calling method continues on the same thread {_handlingThreads[3]}");
            return builder.ToString();
        }

        private async Task DoSomethingAsync()
        {
            _handlingThreads.Add(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            _handlingThreads.Add(Thread.CurrentThread.ManagedThreadId);
        }
    }
}
