using System;
using System.Threading.Tasks;

namespace AsyncAllTheThings._11.UnderTheHood
{
    // Check this out in IL Spy - be sure to have "Show all types and members" enabled to see the async state machine
    public class SampleClass
    {
        public async Task DoSomethingAsync()
        {
            Console.WriteLine("Sync start");
            await IrrelevantAsync();
        }

        public Task DoSomethingWithNoAwaitAsync()
        {
            Console.WriteLine("Sync start");
            return IrrelevantAsync();
        }

        public async Task DoMoreStuffAsync()
        {
            Console.WriteLine("Sync start");
            await IrrelevantAsync();
            Console.WriteLine("After an await");
            await IrrelevantAsync();
            Console.WriteLine("After another await");
            await IrrelevantAsync();
            Console.WriteLine("And another one just for the kicks");
        }

        private Task IrrelevantAsync() => Task.CompletedTask;
    }
}
