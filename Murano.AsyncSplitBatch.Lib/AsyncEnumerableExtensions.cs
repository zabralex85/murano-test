namespace Murano.AsyncSplitBatch.Lib
{
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;
	using System.Threading;
	using System.Threading.Tasks;

	public static class AsyncEnumerableExtensions
	{
		public static async IAsyncEnumerable<List<T>> ToBatchesAsync<T>(
			this IAsyncEnumerable<T> source,
			int batchSize,
			[EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (batchSize <= 0)
				throw new ArgumentOutOfRangeException(nameof(batchSize), "Batch size must be greater than zero.");

			var batch = new List<T>(batchSize);

			await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
			{
				batch.Add(item);

				if (batch.Count == batchSize)
				{
					yield return batch;
					batch = new List<T>(batchSize);
				}
			}

			// Yield any remaining items
			if (batch.Count > 0)
			{
				yield return batch;
			}
		}
	}
}
