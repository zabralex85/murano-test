using System.Runtime.CompilerServices;
using Murano.AsyncSplitBatch.Lib;

namespace Murano.AsyncSplitBatch.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Xunit;

	public class BatchesAsyncTests
	{
		[Fact]
		public async Task ToBatchesAsync_SplitsIntoCorrectBatches()
		{
			// Arrange
			var source = AsyncEnumerable.Range(1, 10); // 1 to 10
			int batchSize = 3;

			// Act
			var result = await source.ToBatchesAsync(batchSize).ToListAsync();

			// Assert
			Assert.Equal(4, result.Count); // Should produce 4 batches
			Assert.Equal([1, 2, 3], result[0]);
			Assert.Equal([4, 5, 6], result[1]);
			Assert.Equal([7, 8, 9], result[2]);
			Assert.Equal([10], result[3]); // Last batch contains 1 item
		}

		[Fact]
		public async Task ToBatchesAsync_EmptySource_ProducesNoBatches()
		{
			// Arrange
			var source = AsyncEnumerable.Empty<int>();
			int batchSize = 3;

			// Act
			var result = await source.ToBatchesAsync(batchSize).ToListAsync();

			// Assert
			Assert.Empty(result); // No batches should be created
		}

		[Fact]
		public async Task ToBatchesAsync_SingleItem_ProducesSingleBatch()
		{
			// Arrange
			var source = AsyncEnumerable.Repeat(42, 1); // Single item: 42
			int batchSize = 3;

			// Act
			var result = await source.ToBatchesAsync(batchSize).ToListAsync();

			// Assert
			Assert.Single(result); // Should have one batch
			Assert.Equal([42], result[0]);
		}

		[Fact]
		public async Task ToBatchesAsync_BatchSizeLargerThanSource_ProducesSingleBatch()
		{
			// Arrange
			var source = AsyncEnumerable.Range(1, 5); // 1 to 5
			int batchSize = 10; // Larger than source size

			// Act
			var result = await source.ToBatchesAsync(batchSize).ToListAsync();

			// Assert
			Assert.Single(result); // Only one batch should be created
			Assert.Equal([1, 2, 3, 4, 5], result[0]);
		}

		[Fact]
		public async Task ToBatchesAsync_Cancellation_ThrowsTaskCanceledException()
		{
			// Arrange
			var source = GetAsyncEnumerable();
			int batchSize = 3;
			using var cts = new CancellationTokenSource();

			// Act & Assert
			cts.Cancel(); // Cancel immediately

			await Assert.ThrowsAsync<TaskCanceledException>(async () =>
			{
				await foreach (var _ in source.ToBatchesAsync(batchSize, cts.Token))
				{
					// Should throw before processing
				}
			});
		}

		private static async IAsyncEnumerable<int> GetAsyncEnumerable([EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			for (int i = 1; i <= 10; i++)
			{
				await Task.Delay(100, cancellationToken); // Simulate work
				yield return i;
			}
		}
	}
}
