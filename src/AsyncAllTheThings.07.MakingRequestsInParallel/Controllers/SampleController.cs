using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncAllTheThings._07.MakingRequestsInParallel.Controllers
{
    // MAKE SURE YOU CAN MAKE THE REQUESTS IN PARALLEL!
    // for instance with Entity Framework, we can't make queries in parallel using the same DbContext, as it's not thread safe
    // so even if we're using two different services, if the same DbContext is injected into both, it may cause issues

    [Route("parallel-requests")]
    public class SampleController : ControllerBase
    {
        private static readonly Uri SampleEndpoint = new Uri("https://httpstat.us/200?sleep=1000");

        private readonly HttpClient _httpClient;

        public SampleController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("in-series")]
        public async Task<string> DoRequestsInSeriesAsync()
        {
            var firstRequest = await _httpClient.GetAsync(SampleEndpoint);
            var secondRequest = await _httpClient.GetAsync(SampleEndpoint);
            return "All done!";
        }

        [Route("in-parallel-1")]
        public async Task<string> DoRequestsInParallelAsync()
        {
            //if the requests are not connected, but we need specific treatment for them,
            //we can request like this and then handle the responses

            var firstRequest = _httpClient.GetAsync(SampleEndpoint);
            var secondRequest = _httpClient.GetAsync(SampleEndpoint);            

            var firstResponse = await firstRequest;
            //do something with the results

            var secondResponse = await secondRequest;
            //do something with the results

            return "All done!";
        }

        [Route("in-parallel-2")]
        public async Task<string> DoRequestsInParallel2Async()
        {
            //if we just want to do something like fire and forget, we could go like

            var firstRequest = _httpClient.GetAsync(SampleEndpoint);
            var secondRequest = _httpClient.GetAsync(SampleEndpoint);

            await Task.WhenAll(firstRequest, secondRequest); //if we just wanted a single task to complete, we could use Task.WhenAny

            return "All done!";
        }

        [Route("in-parallel-3")]
        public async Task<string> DoRequestsInParallel3Async()
        {
            //or maybe we just want to do a bunch o similar requests and aggregate the result
            var someCollection = Enumerable.Range(0, 5);
            var taskList = new List<Task<HttpResponseMessage>>();
            foreach(var something in someCollection)
            {
                //use something for... something
                taskList.Add(_httpClient.GetAsync(SampleEndpoint));
            }
            
            foreach(var responseTask in taskList)
            {
                var response = await responseTask;
                //do something with the response
            }

            return "All done!";
        }

        [Route("in-parallel-3-alt")]
        public async Task<string> DoRequestsInParallel3AltAsync()
        {
            //or maybe we just want to do a bunch o similar requests and aggregate the result
            var someCollection = Enumerable.Range(0, 5);
            var taskList = new List<Task<HttpResponseMessage>>();
            foreach (var something in someCollection)
            {
                //use something for... something
                taskList.Add(_httpClient.GetAsync(SampleEndpoint));
            }

            var responses = await Task.WhenAll(taskList);

            foreach (var response in responses)
            {
                //do something with the response
            }

            return "All done!";
        }
    }
}
