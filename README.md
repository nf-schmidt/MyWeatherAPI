🌦️ My Weather API

This is a simple ASP.NET Core Web API project that acts as a proxy for the Visual Crossing weather service. It demonstrates key backend development concepts including third-party API integration, caching, and secure key management.

This project is based on the Weather API Wrapper Service project from roadmap.sh.
Project URL: https://roadmap.sh/projects/weather-api-wrapper-service

🚀 Features:

-📡 Fetches real-time weather data for any city.
-⚡ Uses an in-memory cache to reduce redundant API calls and improve performance.
-🔒 Securely manages the external API key using the .NET Secret Manager.

⚙️ How to Run Locally:

📋 Prerequisites

.NET 8 SDK

A free API key from Visual Crossing Weather API

🛠️ Setup Instructions

Clone the repository:

git clone https://github.com/nf-schmidt/MyWeatherAPI
cd WeatherApi


Restore dependencies:

dotnet restore


Set up the API Key:
You must store your Visual Crossing API key as a user secret. Run the following command, replacing YOUR_API_KEY with your actual key:

dotnet user-secrets set "ExternalWeatherApi:ApiKey" "YOUR_API_KEY"


Run the application:

dotnet run


The API will be available at https://localhost:5195.

📖 API Usage

📍 Get Weather by City

Endpoint: GET /weather/{city}

Description: Retrieves the weather forecast for the specified city.

Example:

https://localhost:5195/weather/saopaulo
