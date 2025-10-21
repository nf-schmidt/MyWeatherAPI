/*
 * This file is the main entry point for the ASP.NET Core application.
 *
 * Responsibilities:
 * - Creates and configures the web application host.
 * - Registers all necessary services in the dependency injection container (e.g., controllers, caching, HttpClient, Swagger).
 * - Configures the HTTP request pipeline (middleware) to handle incoming requests.
*/

using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// The Swagger UI will only be available in the Development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

