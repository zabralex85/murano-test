using Murano.EnumerableWhere.Lib;

namespace Murano.EnumerableWhere
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			// Use the Where extension method
			var evenNumbers = numbers.WhereBy(n => n % 2 == 0);

			Console.WriteLine("Even Numbers:");
			foreach (var number in evenNumbers)
			{
				Console.WriteLine(number);
			}
		}
	}
}
