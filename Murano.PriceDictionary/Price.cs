namespace Murano.PriceDictionary
{
	public class Price : IEquatable<Price>
	{
		public string Currency { get; }
		public decimal Amount { get; }

		public Price(string currency, decimal amount)
		{
			if (string.IsNullOrWhiteSpace(currency))
			{
				throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
			}

			Currency = currency;
			Amount = amount;
		}

		// Implement equality based on Currency and Amount
		public override bool Equals(object? obj)
		{
			return Equals(obj as Price);
		}

		public bool Equals(Price? other)
		{
			if (other == null) return false;
			return Currency.Equals(other.Currency, StringComparison.Ordinal) && Amount == other.Amount;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Currency, Amount);
		}

		public override string ToString()
		{
			return $"{Currency} {Amount}";
		}
	}
}
