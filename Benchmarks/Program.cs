using BenchmarkDotNet.Running;
using Benchmarks;

BenchmarkRunner.Run<MatrixABench>();
BenchmarkRunner.Run<MatrixBBench>();
BenchmarkRunner.Run<Hilbert4Bench>();
BenchmarkRunner.Run<Hilbert10Bench>();
BenchmarkRunner.Run<Hilbert20Bench>();
BenchmarkRunner.Run<Matrix945Bench>();
BenchmarkRunner.Run<Matrix4545Bench>();