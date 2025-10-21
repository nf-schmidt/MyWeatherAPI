using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace WeatherApi.Services;

public class WeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache,
        IConfiguration configuration,
        ILogger<WeatherService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string?> GetWeatherForCityAsync(string city)
    {
        string cacheKey = $"weather:{city.ToLower()}";

        if (_cache.TryGetValue(cacheKey, out string? cachedWeather))
        {
            _logger.LogInformation("Cache hit for city: {City}", city);
            return cachedWeather;
        }

        _logger.LogInformation("Cache miss for city: {City}. Fetching from API.", city);

        var apiKey = _configuration["ExternalWeatherApi:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            _logger.LogError("External Weather API key is not configured.");
            return null;
        }

        var client = _httpClientFactory.CreateClient();
        var apiUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?unitGroup=metric&key={apiKey}&contentType=json";

        var response = await client.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to fetch weather data for {City}. Status: {StatusCode}", city, response.StatusCode);
            return null;
        }

        string weatherData = await response.Content.ReadAsStringAsync();

        var expiration = TimeSpan.FromHours(12);
        _cache.Set(cacheKey, weatherData, expiration);

        _logger.LogInformation("Successfully cached weather data for {City}", city);

        return weatherData;
    }
}

