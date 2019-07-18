using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AsyncAllTheThings._03.ResultVsGetAwaiterGetResult.Controllers
{
    public class SampleController : ControllerBase
    {
        [HttpGet("when-all-goes-right/result")]
        public ActionResult<string> ResultWhenAllGoesRight()
        {
            var result = DoSomethingAsync(nameof(ResultWhenAllGoesRight)).Result;
            return result;
        }

        [HttpGet("when-all-goes-right/get-awaiter-get-result")]
        public ActionResult<string> GetAwaiterGetResultWhenAllGoesRight()
        {
            var result = DoSomethingAsync(nameof(GetAwaiterGetResultWhenAllGoesRight)).GetAwaiter().GetResult();
            return result;
        }

        [HttpGet("when-it-goes-wrong/result")]
        public ActionResult<string> ResultWhenItGoesWrong()
        {
            try
            {
                var result = FailDoingSomethingAsync().Result;
                return result;
            }
            catch (Exception ex)
            {
                return ex.GetType().Name;
            }

        }

        [HttpGet("when-it-goes-wrong/get-awaiter-get-result")]
        public ActionResult<string> GetAwaiterGetResultWhenItGoesWrong()
        {
            try
            {
                var result = FailDoingSomethingAsync().GetAwaiter().GetResult();
                return result;
            }
            catch (Exception ex)
            {
                return ex.GetType().Name;
            }
        }

        private static async Task<string> DoSomethingAsync(string greeter)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            return $"Hello from {greeter}!";
        }

        private static async Task<string> FailDoingSomethingAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            throw new InvalidOperationException("Kaboom!");
        }

        // probably the only valid scenario to use .Result, is when we're sure the task has completed and is not faulted
    }
}
