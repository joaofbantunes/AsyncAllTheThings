using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AsyncAllTheThings._06.ReturnTaskWithoutAwaiting.Controllers
{
    [Route("return-no-await")]
    public class SampleController : ControllerBase
    {
        [Route("get-something")]
        public Task<string> GetSomethingWithoutAwaiting()
        {
            try
            {
                return BlowUpAfterAwaitingAsync();
            }
            catch (Exception)
            {
                return Task.FromResult("Will never get here...");
            }

            async Task<string> BlowUpAfterAwaitingAsync()
            {
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                throw new InvalidOperationException("Everything you do is invalid! :D");
            }
        }

        [Route("use-disposable")]
        public Task<string> UseSomethingDisposableWithoutAwaiting()
        {
            using (var something = new SomethingDisposable())
            {
                return GetSomethingFromThisDisposable(something);
            }

            async Task<string> GetSomethingFromThisDisposable(SomethingDisposable something)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                return something.GetSomething();
            }
        }

        private class SomethingDisposable : IDisposable
        {
            private bool _disposed;

            public string GetSomething()
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("John Doe");
                }

                return "Howdy!";
            }

            public void Dispose()
            {
                _disposed = true;
            }
        }

        [Route("stack-trace-with-await")]
        public async Task<string> StackTraceAwaitingAsync()
        {
            try
            {
                await CallBlowUpAfterAwaitingAsync();
                return "Not getting here...";
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }

            async Task CallBlowUpAfterAwaitingAsync() => await BlowUpAfterAwaitingAsync();

            async Task BlowUpAfterAwaitingAsync()
            {
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                throw new InvalidOperationException("You insist in this invalid stuff!!");
            }
        }

        [Route("stack-trace-without-await")]
        public async Task<string> StackTraceWithoutAwaitingAsync()
        {
            try
            {
                await CallBlowUpAfterAwaitingAsync();
                return "Not getting here...";
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }

            Task CallBlowUpAfterAwaitingAsync() => BlowUpAfterAwaitingAsync();

            async Task BlowUpAfterAwaitingAsync()
            {
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                throw new InvalidOperationException("You insist in this invalid stuff!!");
            }
        }
    }
}
