namespace Murano.Singleton.Lib
{
	public interface ISingletonService { }

	public sealed class SingletonDI : ISingletonService
	{
		public static SingletonDI Instance { get; } = new SingletonDI();

		private SingletonDI() { }
	}
}
