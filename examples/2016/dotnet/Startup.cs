// ASP.NET Core 1.0 Startup.cs — minimal middleware pipeline.
// Shows the clean composition model: each Use() adds a middleware to the chain.
// No System.Web, no HttpContext in the global sense — everything is per-request.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        // Register your own services here; DI is first-class in Core
        services.AddSingleton<ITranscodeQueue, SqsTranscodeQueue>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                          ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole();

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        // Middleware runs in registration order.
        app.UseMiddleware<RequestTimingMiddleware>();
        app.UseMvc();
    }
}
