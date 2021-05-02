using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAllTheThings._10.CancellationTokens.Controllers
{
    [Route("ct")]
    public class SampleController : ControllerBase
    {
        private static readonly Uri SampleEndpoint = new Uri("https://httpstat.us/200?sleep=10000");
        private static readonly Uri SampleEndpoint2 = new Uri("https://httpstat.us/200");
        private static readonly Uri SampleEndpoint3 = new Uri("https://httpstat.us/200?sleep=10000");

        private readonly HttpClient _httpClient;
        private readonly ILogger<SampleController> _logger;

        public SampleController(HttpClient httpClient, ILogger<SampleController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        [Route("do-something-cancellable")]
        public async Task DoSomethingCancellableAsync(CancellationToken ct)
        {
            try
            {
                await _httpClient.GetAsync(SampleEndpoint, ct);
                _logger.LogDebug("Not canceled!");
            }
            catch (TaskCanceledException)
            {
                _logger.LogDebug("Canceled!");
            }
        }

        // Caution
        // Imagine a request does multiple things, but at some point causes side effects that are not easily cancellable
        public async Task DoSomethingCancellableAndSomethingNotAsync(CancellationToken ct)
        {
            // we're getting some info, sure, let it be canceled
            await _httpClient.GetAsync(SampleEndpoint, ct);

            // if we're causing side effects that can't be easily undone 
            // (adding something to the database in a transaction is easily cancellable, sending an email for instance, isn't)
            // we shouldn't allow for cancellation anymore, to try and avoid inconsistencies
            // (they can still happen in case of failures, but we don't need to make our job even harder :P)
            dynamic somePayload = new { thing = 1 };
            await _httpClient.PostAsync(SampleEndpoint2, somePayload, ct); // the first could have a ct...
            await _httpClient.PostAsync(SampleEndpoint3, somePayload); // but no ct for you!
        }
    }
}