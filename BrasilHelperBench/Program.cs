using BenchmarkDotNet.Running;
using BrasilHelperBench;

BenchmarkRunner.Run<EhCpfValidoBench>();

//BenchmarkRunner.Run<EhCnpjValidoBench>();

Console.WriteLine("\nPressione qualquer tecla para finalizar...");
Console.ReadKey();