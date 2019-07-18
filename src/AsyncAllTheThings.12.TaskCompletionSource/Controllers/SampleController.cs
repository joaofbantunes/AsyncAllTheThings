using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AsyncAllTheThings._12.TaskCompletionSource.Controllers
{
    [Route("tcs")]
    public class SampleController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, TaskCompletionSource<int>> Questions = new ConcurrentDictionary<string, TaskCompletionSource<int>>();

        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
        }

        [Route("ask/{question}")]
        public async Task<IActionResult> AskAsync(string question)
        {
            _logger.LogDebug("Asking...");
            var tcs = Questions.GetOrAdd(question, _ => new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously));
            _logger.LogDebug("Waiting for result...");
            var result = await tcs.Task;
            _logger.LogDebug("Got result!");
            return Content(result.ToString());
        }

        [Route("ask/{question}/tell/{value}")]
        public IActionResult Tell(string question, int value)
        {
            _logger.LogDebug("Telling...");
            if (Questions.TryRemove(question, out var tcs))
            {
                _logger.LogDebug("Setting value...");
                if (!tcs.TrySetResult(value))
                {
                    return StatusCode(500);
                }
                _logger.LogDebug("Value set!");
                return NoContent();
            }

            return NotFound();
        }

        [Route("ask-sync-continuation/{question}")]
        public async Task<IActionResult> AskWithSyncContinuationAsync(string question)
        {
            _logger.LogDebug("Asking...");
            var tcs = Questions.GetOrAdd(question, _ => new TaskCompletionSource<int>());
            _logger.LogDebug("Waiting for result...");
            var result = await tcs.Task;
            _logger.LogDebug("Got result!");
            return Content(result.ToString());
        }
    }
}