using Murano.PriceDictionary;

namespace Murano.PriceDictionaryUse
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var price1 = new Price("USD", 100m);
			var price2 = new Price("USD", 100m);
			var price3 = new Price("EUR", 200m);

			var dictionary = new Dictionary<Price, string>
			{
				[price1] = "Price in USD",
				[price3] = "Price in EUR"
			};

			Console.WriteLine(dictionary[price2]); // Output: Price in USD
			Console.WriteLine(dictionary[price3]); // Output: Price in EUR
		}
	}
}
