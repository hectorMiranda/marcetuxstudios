// .NET 6 Minimal API — no Startup.cs, no controller classes.
// Endpoint filters replace action filters for cross-cutting concerns.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAccountRepository, SqlAccountRepository>();

var app = builder.Build();

// Logging filter applied to a route group
var api = app.MapGroup("/api/v1")
             .AddEndpointFilter<RequestLoggingFilter>();

api.MapGet("/accounts/{id}", async (int id, IAccountRepository repo) =>
{
    var account = await repo.FindAsync(id);
    return account is null
        ? Results.NotFound()
        : Results.Ok(account);
});

api.MapPost("/accounts", async (CreateAccountRequest req, IAccountRepository repo) =>
{
    var id = await repo.CreateAsync(req.OwnerId, req.Name);
    return Results.Created($"/api/v1/accounts/{id}", new { id });
});

app.Run();

// Endpoint filter: logs request/response without touching handler code
record struct RequestLoggingFilter(ILogger<RequestLoggingFilter> Logger)
    : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext ctx,
                                                EndpointFilterDelegate next)
    {
        Logger.LogInformation("→ {Method} {Path}", ctx.HttpContext.Request.Method,
                              ctx.HttpContext.Request.Path);
        var result = await next(ctx);
        Logger.LogInformation("← {StatusCode}", ctx.HttpContext.Response.StatusCode);
        return result;
    }
}

record CreateAccountRequest(int OwnerId, string Name);
