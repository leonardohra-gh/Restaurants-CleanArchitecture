using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Model;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService) : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger = logger;
    private readonly IWeatherForecastService _weatherForecastService = weatherForecastService;

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var result = _weatherForecastService.Get();
        return result;
    }

    [HttpPost("generate")]
    public IActionResult Generate([FromQuery] int numberOfResults, [FromBody] TemperatureRequest temperatureRange)
    {
        if (numberOfResults <= 0)
        {
            return BadRequest("Number of results must be greater than 0.");
        }

        if (temperatureRange.MinimumTemperature > temperatureRange.MaximumTemperature)
        {
            return BadRequest("Minimum temperature must be less than or equal to maximum temperature.");
        }

        var result = _weatherForecastService.Get(numberOfResults, temperatureRange.MinimumTemperature, temperatureRange.MaximumTemperature);
        return Ok(result);
    }
}
