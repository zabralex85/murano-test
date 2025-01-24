using Murano.AsyncSplitBatch.Lib;

namespace Murano.AsyncSplitBatch
{
	internal class Program
	{
		public static async Task Main(string[] args)
		{
			var source = GetNumbersAsync();

			await foreach (var batch in source.ToBatchesAsync(3))
			{
				Console.WriteLine($"Batch: {string.Join(", ", batch)}");
			}
		}

		private static async IAsyncEnumerable<int> GetNumbersAsync()
		{
			for (int i = 1; i <= 10; i++)
			{
				await Task.Delay(100); // Simulate async work
				yield return i;
			}
		}
	}
}
