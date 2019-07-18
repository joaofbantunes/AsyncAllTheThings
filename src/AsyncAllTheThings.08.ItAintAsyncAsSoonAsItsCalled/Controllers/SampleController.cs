using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAllTheThings._08.ItAintAsyncAsSoonAsItsCalled.Controllers
{
    [Route("not-immediately-async")]
    public class SampleController : ControllerBase
    {
        [Route("not-parallel-until-suspension")]
        public async Task<string[]> NotParallelUntilSuspensionAsync()
        {
            var initialThreadId = Thread.CurrentThread.ManagedThreadId;
            var results = await Task.WhenAll(DoSlowThingBeforeSuspendingAsync(), DoSlowThingBeforeSuspendingAsync());
            var endThreadId = Thread.CurrentThread.ManagedThreadId;

            return new[] {
                "Finally done!",
                $"Action started on thread: {initialThreadId}",
                $"First method call started on thread: {results[0].startThreadId}",
                $"Second method call started on thread: {results[1].startThreadId}",
                $"First method call continued on thread: {results[0].continuationThreadId}",
                $"Second method call continued on thread: {results[1].continuationThreadId}",
                $"Action ended on thread: {endThreadId}",
            };

            async Task<(int startThreadId, int continuationThreadId)> DoSlowThingBeforeSuspendingAsync()
            {
                var startThreadId = Thread.CurrentThread.ManagedThreadId;
                Thread.Sleep(1000);
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                var continuationThreadId = Thread.CurrentThread.ManagedThreadId;
                return (startThreadId, continuationThreadId);
            }
        }

        [Route("not-parallel-until-suspension-2")]
        public async Task<string[]> NotParallelUntilSuspension2Async()
        {
            var initialThreadId = Thread.CurrentThread.ManagedThreadId;
            var results = await Task.WhenAll(DoSlowThingAfterSuspendingAsync(), DoSlowThingAfterSuspendingAsync());
            var endThreadId = Thread.CurrentThread.ManagedThreadId;

            return new[] {
                "Seems faster!",
                $"Action started on thread: {initialThreadId}",
                $"First method call started on thread: {results[0].startThreadId}",
                $"Second method call started on thread: {results[1].startThreadId}",
                $"First method call continued on thread: {results[0].continuationThreadId}",
                $"Second method call continued on thread: {results[1].continuationThreadId}",
                $"Action ended on thread: {endThreadId}",
            };

            async Task<(int startThreadId, int continuationThreadId)> DoSlowThingAfterSuspendingAsync()
            {
                var startThreadId = Thread.CurrentThread.ManagedThreadId;
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                Thread.Sleep(1000);
                var continuationThreadId = Thread.CurrentThread.ManagedThreadId;
                return (startThreadId, continuationThreadId);
            }
        }

        [Route("not-parallel-at-all")]
        public async Task<string[]> NotParallelAtAllAsync()
        {
            var initialThreadId = Thread.CurrentThread.ManagedThreadId;
            var results = await Task.WhenAll(DoSlowThingAndNeverSuspendAsync(), DoSlowThingAndNeverSuspendAsync());
            var endThreadId = Thread.CurrentThread.ManagedThreadId;

            return new[] {
                "Sadness :|",
                $"Action started on thread: {initialThreadId}",
                $"First method call started on thread: {results[0].startThreadId}",
                $"Second method call started on thread: {results[1].startThreadId}",
                $"First method call continued on thread: {results[0].continuationThreadId}",
                $"Second method call continued on thread: {results[1].continuationThreadId}",
                $"Action ended on thread: {endThreadId}",
            };

            Task<(int startThreadId, int continuationThreadId)> DoSlowThingAndNeverSuspendAsync()
            {
                var startThreadId = Thread.CurrentThread.ManagedThreadId;
                Thread.Sleep(1000);
                var continuationThreadId = Thread.CurrentThread.ManagedThreadId;
                return Task.FromResult((startThreadId, continuationThreadId));
            }
        }

        [Route("parallel-but-wasting-threads")]
        public async Task<string[]> ParallelButWastingThreadsAsync()
        {
            var initialThreadId = Thread.CurrentThread.ManagedThreadId;
            var results = await Task.WhenAll(DoSlowThingOnAlternativeThreadAsync(), DoSlowThingOnAlternativeThreadAsync());
            var endThreadId = Thread.CurrentThread.ManagedThreadId;

            return new[] {
                "More sadness :|",
                $"Action started on thread: {initialThreadId}",
                $"First method call started on thread: {results[0].startThreadId}",
                $"Second method call started on thread: {results[1].startThreadId}",
                $"First method call continued on thread: {results[0].continuationThreadId}",
                $"Second method call continued on thread: {results[1].continuationThreadId}",
                $"Action ended on thread: {endThreadId}",
            };

            async Task<(int startThreadId, int continuationThreadId)> DoSlowThingOnAlternativeThreadAsync()
            {
                var startThreadId = Thread.CurrentThread.ManagedThreadId;
                await Task.Run(() => Thread.Sleep(1000));
                var continuationThreadId = Thread.CurrentThread.ManagedThreadId;
                return (startThreadId, continuationThreadId);
            }
        }
    }
}