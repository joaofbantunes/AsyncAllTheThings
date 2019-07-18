// * Summary *

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.437 (1809/October2018Update/Redstone5)
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=2.2.300-preview-010050
  [Host]     : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.4 (CoreCLR 4.6.27521.02, CoreFX 4.6.27521.01), 64bit RyuJIT


|                   Method |             Mean |          Error |         StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------- |-----------------:|---------------:|---------------:|-----:|-------:|------:|------:|----------:|
|      AlreadyComputedTask |         21.09 ns |      0.4065 ns |      0.3802 ns |    1 | 0.0114 |     - |     - |      72 B |
| AlreadyComputedValueTask |         23.82 ns |      0.0619 ns |      0.0517 ns |    2 |      - |     - |     - |         - |
|          NotComputedTask | 15,610,258.13 ns | 44,583.7797 ns | 41,703.6952 ns |    3 |      - |     - |     - |     488 B |
|     NotComputedValueTask | 15,611,121.25 ns | 50,959.7884 ns | 47,667.8177 ns |    3 |      - |     - |     - |     520 B |

// * Hints *
Outliers
  ValueTaskBenchmark.AlreadyComputedValueTask: Default -> 2 outliers were removed (25.28 ns, 25.50 ns)

// * Legends *
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Rank      : Relative position of current benchmark mean among all benchmarks (Arabic style)
  Gen 0     : GC Generation 0 collects per 1000 operations
  Gen 1     : GC Generation 1 collects per 1000 operations
  Gen 2     : GC Generation 2 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 ns      : 1 Nanosecond (0.000000001 sec)