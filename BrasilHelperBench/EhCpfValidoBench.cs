using BenchmarkDotNet.Attributes;

namespace BrasilHelperBench;

[RankColumn]
[MemoryDiagnoser]
public class EhCpfValidoBench
{
    [Params(
        "124334680-90",
        "124.334.680-94"
        )]
    public string? Input { get; set; }

    [Benchmark]
    public bool BrasilHelper() { return Marrari.Common.BrasilHelper.EhCpfValido(Input); }  
}