// Registering the Redis backplane for SignalR so all web servers share the
// same message bus. Call this once in OWIN Startup before MapSignalR.
using Microsoft.AspNet.SignalR;
using Owin;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        // Replace with your actual Redis connection string.
        GlobalHost.DependencyResolver.UseRedis(
            host: "redis.internal.example.com",
            port: 6379,
            password: "",
            eventKey: "signalr.backplane");

        // SignalR hub route comes after the backplane is configured.
        app.MapSignalR();
    }
}
