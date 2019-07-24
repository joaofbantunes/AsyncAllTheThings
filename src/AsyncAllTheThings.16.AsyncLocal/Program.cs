using System;
using System.Threading.Tasks;

namespace AsyncAllTheThings._16.AsyncLocal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var asyncLocal = new System.Threading.AsyncLocal<string>();
            asyncLocal.Value = "Before starting tasks";

            var t1 = Task.Run(async () => 
            {
                var initialValue = asyncLocal.Value;                
                await Task.Delay(TimeSpan.FromSeconds(1));
                var intermediateValue = asyncLocal.Value;
                asyncLocal.Value = "Hello from T1";
                return (initialValue: initialValue, intermediateValue: intermediateValue, finalValue: asyncLocal.Value);
            });

            var t2 = Task.Run(async () =>
            {
                var initialValue = asyncLocal.Value;
                await Task.Delay(TimeSpan.FromSeconds(1));
                var intermediateValue = asyncLocal.Value;
                asyncLocal.Value = "Hello from T2";
                return (initialValue: initialValue, intermediateValue: intermediateValue, finalValue: asyncLocal.Value);
            });

            asyncLocal.Value = "After starting tasks";
            var r1 = await t1;
            var r2 = await t2;

            Console.WriteLine("Current value is: {0}", asyncLocal.Value);
            Console.WriteLine("T1 values are:");
            Console.WriteLine("\t- InitialValue: {0}", r1.initialValue);
            Console.WriteLine("\t- IntermediateValue: {0}", r1.intermediateValue);
            Console.WriteLine("\t- FinalValue: {0}", r1.finalValue);
            Console.WriteLine("T2 values are:");
            Console.WriteLine("\t- InitialValue: {0}", r2.initialValue);
            Console.WriteLine("\t- IntermediateValue: {0}", r2.intermediateValue);
            Console.WriteLine("\t- FinalValue: {0}", r2.finalValue);
        }
    }
}
