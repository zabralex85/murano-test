namespace Murano.PriceDictionary.Tests
{
	public class PriceTests
	{
		[Fact]
		public void Price_Equals_SameCurrencyAndAmount_ReturnsTrue()
		{
			// Arrange
			var price1 = new Price("USD", 100m);
			var price2 = new Price("USD", 100m);

			// Act
			var result = price1.Equals(price2);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Price_Equals_DifferentCurrency_ReturnsFalse()
		{
			// Arrange
			var price1 = new Price("USD", 100m);
			var price2 = new Price("EUR", 100m);

			// Act
			var result = price1.Equals(price2);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Price_Equals_DifferentAmount_ReturnsFalse()
		{
			// Arrange
			var price1 = new Price("USD", 100m);
			var price2 = new Price("USD", 200m);

			// Act
			var result = price1.Equals(price2);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Price_GetHashCode_SameCurrencyAndAmount_ProducesSameHashCode()
		{
			// Arrange
			var price1 = new Price("USD", 100m);
			var price2 = new Price("USD", 100m);

			// Act
			var hashCode1 = price1.GetHashCode();
			var hashCode2 = price2.GetHashCode();

			// Assert
			Assert.Equal(hashCode1, hashCode2);
		}

		[Fact]
		public void Price_GetHashCode_DifferentCurrency_ProducesDifferentHashCodes()
		{
			// Arrange
			var price1 = new Price("USD", 100m);
			var price2 = new Price("EUR", 100m);

			// Act
			var hashCode1 = price1.GetHashCode();
			var hashCode2 = price2.GetHashCode();

			// Assert
			Assert.NotEqual(hashCode1, hashCode2);
		}

		[Fact]
		public void Price_GetHashCode_DifferentAmount_ProducesDifferentHashCodes()
		{
			// Arrange
			var price1 = new Price("USD", 100m);
			var price2 = new Price("USD", 200m);

			// Act
			var hashCode1 = price1.GetHashCode();
			var hashCode2 = price2.GetHashCode();

			// Assert
			Assert.NotEqual(hashCode1, hashCode2);
		}

		[Fact]
		public void Dictionary_WithPriceKey_RetrievesCorrectValues()
		{
			// Arrange
			var price1 = new Price("USD", 100m);
			var price2 = new Price("USD", 100m);
			var price3 = new Price("EUR", 200m);

			var dictionary = new Dictionary<Price, string>
			{
				[price1] = "Price in USD",
				[price3] = "Price in EUR"
			};

			// Act
			var value1 = dictionary[price2];
			var value2 = dictionary[price3];

			// Assert
			Assert.Equal("Price in USD", value1);
			Assert.Equal("Price in EUR", value2);
		}

		[Fact]
		public void Price_ToString_ReturnsExpectedFormat()
		{
			// Arrange
			var price = new Price("USD", 100m);

			// Act
			var result = price.ToString();

			// Assert
			Assert.Equal("USD 100", result);
		}

		[Fact]
		public void Price_NullCurrency_ThrowsArgumentException()
		{
			// Act & Assert
			Assert.Throws<ArgumentException>(() => new Price(null, 100m));
		}

		[Fact]
		public void Price_EmptyCurrency_ThrowsArgumentException()
		{
			// Act & Assert
			Assert.Throws<ArgumentException>(() => new Price("", 100m));
		}
	}
}
