// Reads X-Correlation-ID from the inbound request or generates one.
// Adds it to the logging scope so every log line carries it.
// Propagates it outbound via HttpClient (register with AddHeaderPropagation).
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class CorrelationMiddleware
{
    private const string Header = "X-Correlation-ID";
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationMiddleware> _log;

    public CorrelationMiddleware(RequestDelegate next, ILogger<CorrelationMiddleware> log)
    {
        _next = next;
        _log = log;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        var correlationId = ctx.Request.Headers[Header].ToString();
        if (string.IsNullOrEmpty(correlationId))
            correlationId = Guid.NewGuid().ToString("N");

        ctx.Response.Headers[Header] = correlationId;

        using (_log.BeginScope("{CorrelationId}", correlationId))
        {
            await _next(ctx);
        }
    }
}
