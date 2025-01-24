using Murano.CodeSnippet.Implementations.RightImpl.Interfaces;
using Murano.CodeSnippet.Implementations.RightImpl.Models;
using Murano.CodeSnippet.Lib.Implementations.RightImpl.Enums;

namespace Murano.CodeSnippet.Implementations.RightImpl
{
	public sealed class ProductComposer : IComposePrice
	{
		public IList<ProductPrice> ComposeProductPrices(IList<Product> products, IList<Price> prices, CurrencyEnum currency)
		{
			var currencyString = Enum.GetName(typeof(CurrencyEnum), currency);
			var priceLookup = prices
				.Where(p => p.Currency == currencyString)
				.ToLookup(p => p.ProductId);

			var seenKeys = new HashSet<ProductPriceKey>(new ProductPriceKeyComparer());
			var productPrices = new List<ProductPrice>(products.Count * 2);

			foreach (var product in products)
			{
				foreach (var price in priceLookup[product.Id])
				{
					// Use a struct key to represent the composite key
					var key = new ProductPriceKey(price.Amount, product.Name);

					if (seenKeys.Add(key))
					{
						// Create and add a new ProductPrice object
						productPrices.Add(new ProductPrice
						{
							Amount = price.Amount,
							ProductName = product.Name
						});
					}
				}
			}

			return productPrices;
		}

		private readonly struct ProductPriceKey(decimal amount, string productName)
		{
			public decimal Amount { get; } = amount;
			public string ProductName { get; } = productName;
		}

		private sealed class ProductPriceKeyComparer : IEqualityComparer<ProductPriceKey>
		{
			public bool Equals(ProductPriceKey x, ProductPriceKey y)
			{
				return x.Amount == y.Amount && string.Equals(x.ProductName, y.ProductName, StringComparison.Ordinal);
			}

			public int GetHashCode(ProductPriceKey obj)
			{
				return HashCode.Combine(obj.Amount, obj.ProductName);
			}
		}
	}
}
