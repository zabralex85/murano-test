namespace Murano.Singleton.Lib
{
	public sealed class SingletonThreadLocal
	{
		private static readonly ThreadLocal<SingletonThreadLocal> _threadInstance =
			new ThreadLocal<SingletonThreadLocal>(() => new SingletonThreadLocal());

		private SingletonThreadLocal()
		{
		}

		public static SingletonThreadLocal Instance => _threadInstance.Value;
	}
}