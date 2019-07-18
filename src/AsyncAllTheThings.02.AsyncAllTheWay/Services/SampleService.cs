using System;
using System.Threading.Tasks;

namespace AsyncAllTheThings._02.AsyncAllTheWay.Services
{
    public class SampleService : ISampleService
    {
        private static readonly Random RandomGenerator = new Random();

        public async Task<int> GetSomethingAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            return RandomGenerator.Next();
        }

        public async Task<int> GetSomethingProtectedFromHammeringAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            return RandomGenerator.Next();
        }

        public async Task<int> GetSomethingProtectedFromHammeringWithExceptionAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            throw new InvalidOperationException("Dunno what you're doing, but it's surely invalid!");
        }
    }
}