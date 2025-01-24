using Murano.CodeSnippet.Implementations.RightImpl;
using Murano.CodeSnippet.Implementations.RightImpl.Interfaces;
using Murano.CodeSnippet.Lib.Implementations.RightImpl.Enums;
using Price = Murano.CodeSnippet.Implementations.RightImpl.Models.Price;
using Product = Murano.CodeSnippet.Implementations.RightImpl.Models.Product;

namespace Murano.CodeSnippet.Tests
{
	public class RightImplTest
	{
		private readonly IComposePrice _priceComposer;

		public RightImplTest()
		{
			_priceComposer = new ProductComposer();
		}

		[Fact]
		public void ComposeProductRubPrices_ShouldReturnCorrectProductRubPrices_WhenProductsAndPricesMatch()
		{
			// Arrange
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
			var result = _priceComposer.ComposeProductPrices(products, prices, CurrencyEnum.RUB);

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
			var result = _priceComposer.ComposeProductPrices(products, prices, CurrencyEnum.RUB);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void ComposeProductRubPrices_ShouldReturnDistinctProductRubPrices_WhenDuplicatePricesExist()
		{
			// Arrange
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
			var result = _priceComposer.ComposeProductPrices(products, prices, CurrencyEnum.RUB);

			// Assert
			Assert.Single(result);
			Assert.Equal("Product1", result[0].ProductName);
			Assert.Equal(100, result[0].Amount);
		}

		[Fact]
		public void ComposeProductRubPrices_ShouldReturnEmptyList_WhenProductsListIsEmpty()
		{
			// Arrange
			var products = new List<Product>();
			var prices = new List<Price>
			{
				new Price { ProductId = 1, Amount = 100, Currency = "RUB" }
			};

			// Act
			var result = _priceComposer.ComposeProductPrices(products, prices, CurrencyEnum.RUB);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void ComposeProductRubPrices_ShouldReturnEmptyList_WhenPricesListIsEmpty()
		{
			// Arrange
			var products = new List<Product>
			{
				new Product { Id = 1, Name = "Product1" }
			};
			var prices = new List<Price>();

			// Act
			var result = _priceComposer.ComposeProductPrices(products, prices, CurrencyEnum.RUB);

			// Assert
			Assert.Empty(result);
		}
	}
}
