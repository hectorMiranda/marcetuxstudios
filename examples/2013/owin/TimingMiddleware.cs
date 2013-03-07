// A minimal OWIN middleware component. Measures request duration and pushes
// a log entry to Redis. No HttpContext, no IIS coupling.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using StackExchange.Redis;

public class TimingMiddleware
{
    private readonly Func<IDictionary<string, object>, Task> _next;
    private readonly IDatabase _redis;

    public TimingMiddleware(Func<IDictionary<string, object>, Task> next, IDatabase redis)
    {
        _next  = next;
        _redis = redis;
    }

    public async Task Invoke(IDictionary<string, object> env)
    {
        var sw = Stopwatch.StartNew();
        await _next(env);
        sw.Stop();

        var path   = env["owin.RequestPath"] as string ?? "/";
        var status = env["owin.ResponseStatusCode"] is int s ? s : 200;
        var entry  = $"{DateTime.UtcNow:O} {status} {path} {sw.ElapsedMilliseconds}ms";

        // Fire-and-forget to a list; a separate consumer tails it.
        await _redis.ListRightPushAsync("request-log", entry);
    }
}
