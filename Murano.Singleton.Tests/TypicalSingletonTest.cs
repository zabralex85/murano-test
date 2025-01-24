using Microsoft.Extensions.DependencyInjection;

namespace Murano.Singleton.Tests
{
	public class TypicalSingletonTest
	{
		[Fact]
		public void GetWeatherForecast_ReturnsFiveForecasts()
		{
			// Arrange
			var service = new WeatherForecastService();

			// Act
			var forecasts = service.GetWeatherForecast().ToList();

			// Assert
			Assert.Equal(5, forecasts.Count); // Should return exactly 5 forecasts
		}

		[Fact]
		public void GetWeatherForecast_HasValidTemperatureRange()
		{
			// Arrange
			var service = new WeatherForecastService();

			// Act
			var forecasts = service.GetWeatherForecast();

			// Assert
			Assert.All(forecasts, forecast =>
			{
				Assert.InRange(forecast.TemperatureC, -20, 55); // Validate temperature range
			});
		}

		[Fact]
		public void GetWeatherForecast_HasValidSummary()
		{
			// Arrange
			var service = new WeatherForecastService();
			var validSummaries = new[]
			{
				"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
			};

			// Act
			var forecasts = service.GetWeatherForecast();

			// Assert
			Assert.All(forecasts, forecast =>
			{
				Assert.Contains(forecast.Summary, validSummaries); // Validate summary is from the predefined set
			});
		}
	
		[Fact]
		public void WeatherForecastService_IsSingleton()
		{
			// Arrange
			var services = new ServiceCollection();
			services.AddSingleton<WeatherForecastService>();

			var serviceProvider = services.BuildServiceProvider();

			// Act
			var instance1 = serviceProvider.GetService<WeatherForecastService>();
			var instance2 = serviceProvider.GetService<WeatherForecastService>();

			// Assert
			Assert.NotNull(instance1);
			Assert.NotNull(instance2);
			Assert.Same(instance1, instance2); // Ensure both references point to the same instance
		}

		[Fact]
		public void WeatherForecastService_AlwaysReturnsSameInstance()
		{
			// Arrange
			var services = new ServiceCollection();

			// Register the service multiple times
			services.AddSingleton<WeatherForecastService>();
			services.AddSingleton<WeatherForecastService>();
			services.AddSingleton<WeatherForecastService>();

			var serviceProvider = services.BuildServiceProvider();

			// Act
			var instance1 = serviceProvider.GetService<WeatherForecastService>();
			var instance2 = serviceProvider.GetService<WeatherForecastService>();
			var instance3 = serviceProvider.GetService<WeatherForecastService>();

			// Assert
			Assert.NotNull(instance1);
			Assert.NotNull(instance2);
			Assert.NotNull(instance3);
			Assert.Same(instance1, instance2); // All instances must be the same
			Assert.Same(instance2, instance3);
		}
	}
}
