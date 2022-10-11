using BenchmarkDotNet.Attributes;

namespace BrasilHelperBench;

[RankColumn]
[MemoryDiagnoser]
public class EhCnpjValidoBench
{
    [Params(
        "09480598/0001-39",
        "09.480.598/0001-38"
        )]
    public string? Input { get; set; }

    [Benchmark]
    public bool BrasilHelper() { return Marrari.Common.BrasilHelper.EhCpfValido(Input); }
}