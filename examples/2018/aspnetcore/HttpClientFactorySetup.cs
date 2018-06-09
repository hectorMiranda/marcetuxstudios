// .NET Core 2.1 - IHttpClientFactory typed client pattern.
// Avoids socket exhaustion and handles connection pooling correctly.
// All retry/auth logic lives in the delegating handler, not the service.
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// Typed client - dependency-injected, factory manages the pool
public class GdsApiClient
{
    private readonly HttpClient _http;
    public GdsApiClient(HttpClient http) { _http = http; }

    public async Task<string> GetFaresAsync(string origin, string destination)
    {
        var response = await _http.GetAsync($"/fares?from={origin}&to={destination}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}

// Registration in Startup.ConfigureServices
// services.AddHttpClient<GdsApiClient>(c =>
// {
//     c.BaseAddress = new Uri("https://gds.example.com");
//     c.DefaultRequestHeaders.Add("Accept", "application/json");
// })
// .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1)));
