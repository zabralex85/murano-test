namespace Murano.CodeSnippet.Implementations.WrongImpl
{
	public class Test
	{
		public IList<ProductRubPrice> ComposeProductRubPrices(IList<Product> products, IList<Price> prices)
		{
			var productPrices = new List<ProductRubPrice>();

			foreach (var product in products)
			{
				var filteredPrices = prices.Where(p => p.ProductId == product.Id && p.Currency == "RUB")
					.Select(p => new ProductRubPrice { Amount = p.Amount, ProductName = product.Name })
					.ToList();

				if (filteredPrices.Any())
				{
					productPrices.AddRange(filteredPrices);
				}
			}

			return productPrices.Distinct().ToList();
		}
	}

	public class Product
	{
		public int Id;
		public string Name;
	}

	public class Price
	{
		public int ProductId;
		public decimal Amount;
		public string Currency;
	}

	public class ProductRubPrice
	{
		public string ProductName;
		public decimal Amount;
	}

}
