using BenchmarkDotNet.Running;

namespace Murano.CodeSnippet.LoadTest
{
	public class Program
	{
		static void Main(string[] args)
		{
			/*
			 * | Method                                 | Mean     | Error     | StdDev    | Gen0     | Gen1     | Gen2     | Allocated |
			   |--------------------------------------- |---------:|----------:|----------:|---------:|---------:|---------:|----------:|
			   | ComposeProductRubPrices_Right_LoadTest | 6.844 ms | 0.0362 ms | 0.0339 ms | 546.8750 | 328.1250 | 109.3750 |    3.2 MB |
			 */
			BenchmarkRunner.Run<RightImplLoadTest>();

			/*
			 *
			 * | Method                                 | Mean    | Error    | StdDev   | Allocated |
			   |--------------------------------------- |--------:|---------:|---------:|----------:|
			   | ComposeProductRubPrices_Wrong_LoadTest | 1.304 s | 0.0261 s | 0.0490 s |   8.24 MB |
			 */
			BenchmarkRunner.Run<WrongImplLoadTest>();
		}
	}
}
