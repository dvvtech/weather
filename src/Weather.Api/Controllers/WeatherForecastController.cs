using Microsoft.AspNetCore.Mvc;

namespace Weather.Api.Controllers;

[ApiController]
[Route("weatherforecast")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var secretsPath = "/run/secrets";
        var ipFile = Path.Combine(secretsPath, "test_key");
        if (System.IO.File.Exists(ipFile))
        {
            var secret = System.IO.File.ReadAllText(ipFile).Trim();
            _logger.LogInformation("secret: " + secret);
        }
        else
        {
            _logger.LogInformation("not found");
        }

        _logger.LogInformation("hello");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
