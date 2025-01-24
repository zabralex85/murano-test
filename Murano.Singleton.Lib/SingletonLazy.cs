namespace Murano.Singleton.Lib
{
	public sealed class SingletonLazy
	{
		private static SingletonLazy _instance;
		private static readonly object _lock = new object();

		private SingletonLazy()
		{
		}

		public static SingletonLazy Instance
		{
			get
			{
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = new SingletonLazy();
					}

					return _instance;
				}
			}
		}
	}
}