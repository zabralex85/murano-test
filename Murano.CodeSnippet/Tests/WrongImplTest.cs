using Murano.CodeSnippet.Implementations.WrongImpl;

namespace Murano.CodeSnippet.Tests
{
	public class WrongImplTest
	{
		[Fact]
		public void ComposeProductRubPrices_ShouldReturnCorrectProductRubPrices_WhenProductsAndPricesMatch()
		{
			// Arrange
			var testClass = new Test();
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Product1" },
				new Product { Id = 2, Name = "Product2" }
			};

			var prices = new List<Price>
			{
				new Price { ProductId = 1, Amount = 100, Currency = "RUB" },
				new Price { ProductId = 2, Amount = 200, Currency = "RUB" },
				new Price { ProductId = 1, Amount = 150, Currency = "USD" } // Non-RUB price
            };

			// Act
			var result = testClass.ComposeProductRubPrices(products, prices);

			// Assert
			Assert.Equal(2, result.Count);

			var product1Price = result.FirstOrDefault(p => p.ProductName == "Product1");
			Assert.NotNull(product1Price);
			Assert.Equal(100, product1Price.Amount);

			var product2Price = result.FirstOrDefault(p => p.ProductName == "Product2");
			Assert.NotNull(product2Price);
			Assert.Equal(200, product2Price.Amount);
		}

		[Fact]
		public void ComposeProductRubPrices_ShouldReturnEmptyList_WhenNoMatchingPricesExist()
		{
			// Arrange
			var testClass = new Test();
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Product1" },
				new Product { Id = 2, Name = "Product2" }
			};

			var prices = new List<Price>
			{
				new Price { ProductId = 1, Amount = 100, Currency = "USD" },
				new Price { ProductId = 2, Amount = 200, Currency = "EUR" }
			};

			// Act
			var result = testClass.ComposeProductRubPrices(products, prices);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void ComposeProductRubPrices_ShouldReturnDistinctProductRubPrices_WhenDuplicatePricesExist()
		{
			// Arrange
			var testClass = new Test();
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Product1" }
			};

			var prices = new List<Price>
			{
				new Price { ProductId = 1, Amount = 100, Currency = "RUB" },
				new Price { ProductId = 1, Amount = 100, Currency = "RUB" } // Duplicate price
            };

			// Act
			var result = testClass.ComposeProductRubPrices(products, prices);

			// Assert
			Assert.Single(result);
			Assert.Equal("Product1", result[0].ProductName);
			Assert.Equal(100, result[0].Amount);
		}

		[Fact]
		public void ComposeProductRubPrices_ShouldReturnEmptyList_WhenProductsListIsEmpty()
		{
			// Arrange
			var testClass = new Test();
			var products = new List<Product>();
			var prices = new List<Price>
			{
				new Price { ProductId = 1, Amount = 100, Currency = "RUB" }
			};

			// Act
			var result = testClass.ComposeProductRubPrices(products, prices);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void ComposeProductRubPrices_ShouldReturnEmptyList_WhenPricesListIsEmpty()
		{
			// Arrange
			var testClass = new Test();
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Product1" }
			};
			var prices = new List<Price>();

			// Act
			var result = testClass.ComposeProductRubPrices(products, prices);

			// Assert
			Assert.Empty(result);
		}
	}
}
