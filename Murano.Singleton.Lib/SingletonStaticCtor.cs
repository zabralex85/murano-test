namespace Murano.Singleton.Lib
{
	public sealed class SingletonStaticCtor
	{
		private static readonly SingletonStaticCtor _instance;

		static SingletonStaticCtor()
		{
			_instance = new SingletonStaticCtor();
		}

		private SingletonStaticCtor()
		{
		}

		public static SingletonStaticCtor Instance => _instance;
	}
}