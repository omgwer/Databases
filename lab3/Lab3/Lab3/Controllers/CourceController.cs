using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers;

[ApiController]
[Route("[controller]")]
public class CourceController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "ds"
            })
            .ToArray();
    }
}