var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/weatherforecast", (WeatherForecastService weatherService) =>
{
	var forecast = weatherService.GetWeatherForecast();
	return forecast;
});

app.Run();