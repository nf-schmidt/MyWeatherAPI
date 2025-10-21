/*
 * This file defines the API Controller for weather-related endpoints.
 *
 * Responsibilities:
 * - Defines the routes for the API (e.g., "/weather/{city}").
 * - Handles incoming HTTP requests.
 * - Validates user input.
 * - Delegates the business logic to the WeatherService.
 * - Returns appropriate HTTP status codes and responses (e.g., 200 OK, 404 Not Found, 400 Bad Request).
*/

using Microsoft.AspNetCore.Mvc;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;
    private readonly ILogger<WeatherController> _logger;

    // 2. The controller now asks for the WeatherService
    public WeatherController(WeatherService weatherService, ILogger<WeatherController> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> Get(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return BadRequest("City name cannot be empty.");
        }

        // Calls the service to get the real data
        var weatherData = await _weatherService.GetWeatherForCityAsync(city);

        if (weatherData == null)
        {
            return NotFound($"Could not retrieve weather data for {city}.");
        }

        // Returns the raw JSON data from the service
        return Content(weatherData, "application/json");
    }
}

