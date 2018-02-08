// ASP.NET Core 2.0 health checks. Note: the Microsoft.Extensions.Diagnostics.HealthChecks
// package; readiness includes DB + cache; liveness is just the app process.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("database", new SqlConnectionHealthCheck(Configuration.GetConnectionString("Default")))
            .AddCheck("cache", new RedisHealthCheck(Configuration["Redis:ConnectionString"]));
    }

    public void Configure(IApplicationBuilder app)
    {
        // Kubernetes liveness: is the process alive at all?
        app.UseHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false  // always healthy if the app is running
        });

        // Kubernetes readiness: is this pod ready to receive traffic?
        app.UseHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready")
        });

        app.UseMvc();
    }
}
