namespace Murano.EnumerableWhere.Lib
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<TSource> WhereBy<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			foreach (var item in source)
			{
				if (predicate(item))
				{
					yield return item;
				}
			}
		}
	}
}
