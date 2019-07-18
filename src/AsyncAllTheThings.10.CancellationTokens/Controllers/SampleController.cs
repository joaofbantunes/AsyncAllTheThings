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
                _logger.LogDebug("Not cancelled!");
            }
            catch (TaskCanceledException)
            {
                _logger.LogDebug("Cancelled!");
            }
        }

        // Caution
        // Imagine a request does multiple things, but at some point causes side effects that are not easily cancellable
        public async Task DoSomethingCancellableAndSomethingNotAsync(CancellationToken ct)
        {
            // we're getting some info, sure, let it be cancelled
            await _httpClient.GetAsync(SampleEndpoint, ct);

            // if we're causing side effects that can't be easily undone (like adding something to the database in a transaction)
            // we shouldn't allow for cancellation anymore, to try and avoid inconsistencies
            // (they can still happen in case of failures, but we don't need to make our job even harder :P)
            dynamic somePayload = new { thing = 1 };
            await _httpClient.PostAsync(SampleEndpoint, somePayload); // no ct for you!
        }
    }
}