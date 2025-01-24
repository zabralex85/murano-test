using Murano.CodeSnippet.Implementations.RightImpl.Models;
using Murano.CodeSnippet.Lib.Implementations.RightImpl.Enums;

namespace Murano.CodeSnippet.Implementations.RightImpl.Interfaces
{
	public interface IComposePrice
	{
		public IList<ProductPrice> ComposeProductPrices(IList<Product> products, IList<Price> prices, CurrencyEnum currency);
	}
}