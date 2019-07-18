using AsyncAllTheThings._14.DuckTyping.Ducks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AsyncAllTheThings._14.DuckTyping.Controllers
{
    [Route("duck-typing")]
    public class SampleController : ControllerBase
    {
        [Route("instance")]
        public async Task<ActionResult<int>> DuckTypedInstanceAsync()
        {
            return await new RandomAfterDelayAwaitableSample(TimeSpan.FromMilliseconds(15));
        }

        [Route("extension")]
        public async Task<ActionResult<int[]>> DuckTypedExtensionAsync()
        {
            var random = new Random();

            return await new[]
            {
                GetRandomAfterDelay(TimeSpan.FromMilliseconds(10), random),
                GetRandomAfterDelay(TimeSpan.FromMilliseconds(20), random),
                GetRandomAfterDelay(TimeSpan.FromMilliseconds(30), random)
            };
        }

        private static async Task<int> GetRandomAfterDelay(TimeSpan delay, Random random)
        {
            await Task.Delay(delay);
            return random.Next();
        }
    }
}