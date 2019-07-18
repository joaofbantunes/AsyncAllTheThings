// * Summary *

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.437 (1809/October2018Update/Redstone5)
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=2.2.300-preview-010050
  [Host]     : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT


|   Method |     Mean |     Error |    StdDev | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |---------:|----------:|----------:|-----:|------:|------:|------:|----------:|
|    Await | 15.60 ms | 0.0971 ms | 0.0860 ms |    1 |     - |     - |     - |     496 B |
| NoAwait1 | 15.63 ms | 0.0918 ms | 0.0859 ms |    1 |     - |     - |     - |     376 B |
| NoAwait2 | 15.61 ms | 0.0704 ms | 0.0659 ms |    1 |     - |     - |     - |     264 B |

// * Hints *
Outliers
  AsyncBenchmark.Await: Default -> 1 outlier  was  removed (15.91 ms)

// * Legends *
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Rank      : Relative position of current benchmark mean among all benchmarks (Arabic style)
  Gen 0     : GC Generation 0 collects per 1000 operations
  Gen 1     : GC Generation 1 collects per 1000 operations
  Gen 2     : GC Generation 2 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 ms      : 1 Millisecond (0.001 sec)