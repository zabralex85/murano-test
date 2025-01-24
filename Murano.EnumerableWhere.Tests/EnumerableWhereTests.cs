using Murano.EnumerableWhere.Lib;

namespace Murano.EnumerableWhereBy.Tests
{
	public class EnumerableWhereByTests
	{
		[Fact]
		public void WhereBy_FiltersCorrectly()
		{
			// Arrange
			var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			// Act
			var result = numbers.WhereBy(n => n % 2 == 0).ToList();

			// Assert
			Assert.Equal([2, 4, 6, 8, 10], result);
		}

		[Fact]
		public void WhereBy_EmptySource_ReturnsEmpty()
		{
			// Arrange
			var numbers = new List<int>();

			// Act
			var result = numbers.WhereBy(n => n % 2 == 0).ToList();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void WhereBy_NoMatchingItems_ReturnsEmpty()
		{
			// Arrange
			var numbers = new List<int> { 1, 3, 5, 7, 9 };

			// Act
			var result = numbers.WhereBy(n => n % 2 == 0).ToList();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void WhereBy_AllItemsMatch_ReturnsAll()
		{
			// Arrange
			var numbers = new List<int> { 2, 4, 6, 8, 10 };

			// Act
			var result = numbers.WhereBy(n => n % 2 == 0).ToList();

			// Assert
			Assert.Equal(numbers, result);
		}

		[Fact]
		public void WhereBy_NullSource_ThrowsArgumentNullException()
		{
			// Arrange
			List<int> numbers = null;

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => numbers.WhereBy(n => n % 2 == 0).ToList());
		}

		[Fact]
		public void WhereBy_NullPredicate_ThrowsArgumentNullException()
		{
			// Arrange
			var numbers = new List<int> { 1, 2, 3 };

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => numbers.WhereBy(null).ToList());
		}
	}
}
