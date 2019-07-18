using AsyncAllTheThings._02.AsyncAllTheWay.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAllTheThings._02.AsyncAllTheWay.Controllers
{
    [RoutePrefix("path")]
    public class SampleController : ApiController
    {
        private readonly ISampleService _sampleService;

        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [HttpGet]
        [Route("sad")]
        public int SadPath()
        {
            return _sampleService.GetSomethingAsync().Result;
        }

        [HttpGet]
        [Route("happy")]
        public async Task<int> HappyPath()
        {
            return await _sampleService.GetSomethingAsync();
        }

        [HttpGet]
        [Route("almost-as-sad")]
        public int AlmostAsSadPath()
        {
            // GetSomethingProtectedFromHammeringAsync doesn't need the context, so it won't block
            // (we'll see how in another project)
            return _sampleService
                .GetSomethingProtectedFromHammeringAsync()
                .Result;
        }

        [HttpGet]
        [Route("almost-as-sad-but-a-little-less")]
        public int AlmostAsSadButALittleLessPath()
        {
            // GetSomethingProtectedFromHammeringAsync doesn't need the context, so it won't block
            // (we'll see how in another project)
            return _sampleService
                .GetSomethingProtectedFromHammeringAsync()
                .GetAwaiter()
                .GetResult();
        }

        [HttpGet]
        [Route("almost-as-sad-with-exception")]
        public string AlmostAsSadWithExceptionPath()
        {
            try
            {
                // GetSomethingProtectedFromHammeringAsync doesn't need the context, so it won't block
                // (we'll see how in another project)
                return _sampleService
                    .GetSomethingProtectedFromHammeringWithExceptionAsync()
                    .Result
                    .ToString();
            }
            catch (Exception ex)
            {
                return ex.GetType().FullName;
            }
        }

        [HttpGet]
        [Route("almost-as-sad-but-a-little-less-with-exception")]
        public string AlmostAsSadButALittleLessWithExceptionPath()
        {
            try
            {
                // GetSomethingProtectedFromHammeringAsync doesn't need the context, so it won't block
                // (we'll see how in another project)
                return _sampleService
                    .GetSomethingProtectedFromHammeringWithExceptionAsync()
                    .GetAwaiter()
                    .GetResult()
                    .ToString();
            }
            catch (Exception ex)
            {
                return ex.GetType().FullName;
            }
        }
    }
}