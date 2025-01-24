namespace Murano.Singleton.Lib
{
	public sealed class SingletonDoubleCheck
	{
		private static SingletonDoubleCheck _instance;
		private static readonly object _lock = new object();

		private SingletonDoubleCheck()
		{
		}

		public static SingletonDoubleCheck Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						if (_instance == null)
						{
							_instance = new SingletonDoubleCheck();
						}
					}
				}

				return _instance;
			}
		}
	}
}