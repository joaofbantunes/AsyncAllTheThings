using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AsyncAllTheThings._13.CancellationTokenSource.Controllers
{
    [Route("cts")]
    public class SampleController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, System.Threading.CancellationTokenSource> Procrastinators = new ConcurrentDictionary<string, System.Threading.CancellationTokenSource>();

        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
        }

        [Route("procrastinate/{seconds}")]
        public async Task<IActionResult> ProcrastinateAsync(int seconds)
        {
            _logger.LogDebug("Will procrastinate for {seconds} seconds", seconds);
            var id = Guid.NewGuid().ToString();
            using (var cts = new System.Threading.CancellationTokenSource())
            {

                if (!Procrastinators.TryAdd(id, cts))
                {
                    return StatusCode(500, "Something weird happened...");
                }

                _logger.LogDebug("Procrastinating...");
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(seconds), cts.Token);
                    if (!Procrastinators.TryRemove(id, out _))
                    {
                        return StatusCode(500, "Something weird happened...");
                    }
                    _logger.LogDebug("All done!");

                }
                catch (TaskCanceledException)
                {
                    _logger.LogDebug("Uh oh! Caught in the act 😲");
                    return StatusCode(500, "Uh oh! Caught in the act 😲");
                }
                return Content("All done!");
            }
        }

        [Route("yell")]
        public IActionResult Yell()
        {
            foreach (var procrastinatorId in Procrastinators.Keys)
            {
                if (Procrastinators.TryRemove(procrastinatorId, out var cts))
                {
                    _logger.LogDebug("Yelling at {procrastinatorId}", procrastinatorId);
                    cts.Cancel();
                }
            }

            return NoContent();
        }

    }
}