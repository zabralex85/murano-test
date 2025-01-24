namespace Murano.Singleton.Lib
{
	public sealed class SingletonLazyT
	{
		private static readonly Lazy<SingletonLazyT> _lazyInstance =
			new Lazy<SingletonLazyT>(() => new SingletonLazyT());

		private SingletonLazyT()
		{
		}

		public static SingletonLazyT Instance => _lazyInstance.Value;
	}
}