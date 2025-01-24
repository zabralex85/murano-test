using BenchmarkDotNet.Attributes;
using Murano.CodeSnippet.Implementations.RightImpl;
using Murano.CodeSnippet.Implementations.RightImpl.Models;
using Murano.CodeSnippet.Lib.Implementations.RightImpl.Enums;

namespace Murano.CodeSnippet.LoadTest
{
	[MemoryDiagnoser]
	public class RightImplLoadTest
	{
		private List<Product> _products;
		private List<Price> _prices;
		private readonly ProductComposer _composer;

		public RightImplLoadTest()
		{
			_composer = new ProductComposer();
		}

		[GlobalSetup]
		public void Setup()
		{
			// Generate 10,000 products
			_products = Enumerable.Range(1, 10000)
				.Select(i => new Product { Id = i, Name = $"Product{i}" })
				.ToList();

			// Generate 100,000 prices (10 prices per product, mix of RUB and other currencies)
			_prices = Enumerable.Range(1, 100000)
				.Select(i => new Price
				{
					ProductId = i % 10000 + 1,
					Amount = i % 100 + 1,
					Currency = i % 2 == 0 ? "RUB" : "USD"
				})
				.ToList();
		}

		[Benchmark]
		public void ComposeProductRubPrices_Right_LoadTest()
		{
			_composer.ComposeProductPrices(_products, _prices, CurrencyEnum.RUB);
		}
	}
}