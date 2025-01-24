namespace Murano.Singleton.Lib
{
	public sealed class SingletonEager
	{
		private static readonly SingletonEager _instance = new SingletonEager();

		private SingletonEager()
		{
		}

		public static SingletonEager Instance => _instance;
	}
}