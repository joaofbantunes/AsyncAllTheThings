using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AsyncAllTheThings._05.ConfigureAwait.Controllers
{
    [RoutePrefix("configure-await-false")]
    public class SampleController : ApiController
    {
        [HttpGet]
        [Route("no-more-deadlocks")]
        public string NoMoreDeadlocks()
        {
            DoSomethingAsync().GetAwaiter().GetResult();
            return "Yay!";
        }

        [HttpGet]
        [Route("but-we-lose-context")]
        public async Task<string> LoseContextAsync()
        {
            var existsHttpContextBefore = HttpContext.Current != null;
            var result = await DoSomethingAsync();
            var existsHttpContextAfter = HttpContext.Current != null;

            var builder = new StringBuilder();
            builder.AppendLine($"The caller maintains the context: {existsHttpContextBefore} -> {existsHttpContextAfter}");
            builder.AppendLine($"But not the called async method: {result.existsHttpContextBefore} -> {result.existsHttpContextAfter}");
            builder.AppendLine("(Which may or may not matter)");
            return builder.ToString();
        }

        [HttpGet]
        [Route("back-to-deadlocks")]
        public string BackToDeadlocks()
        {
            // ConfigureAwait(false) is only effective against this when blocking directly on the configured await
            CallDoSomethingAsync().GetAwaiter().GetResult();
            return "Meh!";
        }

        private async Task<(bool existsHttpContextBefore, bool existsHttpContextAfter)> DoSomethingAsync()
        {
            var existsHttpContextBefore = HttpContext.Current != null;
            await Task.Delay(TimeSpan.FromMilliseconds(500)).ConfigureAwait(false);
            var existsHttpContextAfter = HttpContext.Current != null;
            return (existsHttpContextBefore, existsHttpContextAfter);
        }

        private async Task<(bool existsHttpContextBefore, bool existsHttpContextAfter)> CallDoSomethingAsync()
        {
            return await DoSomethingAsync();
        }
    }
}
