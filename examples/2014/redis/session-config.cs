// Configure ASP.NET to use Redis for session state instead of SQL Server.
// Requires StackExchange.Redis.Extensions.AspNet from NuGet.
// Session keys expire automatically — no SQL cleanup job needed.

// In Global.asax.cs or Startup:
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;

public static class SessionConfig
{
    public static void Register()
    {
        // One connection multiplexer shared across the app — it's thread-safe.
        var redis = ConnectionMultiplexer.Connect("localhost:6379");

        // Swap the session provider; existing session code is unchanged.
        // SessionStateMode.Custom with the Redis provider handles distribution.
        // Sessions expire in 20 minutes of inactivity; Redis evicts the key.
        System.Web.HttpContext.Current.Application["RedisConnection"] = redis;
    }
}
// web.config snippet:
// <sessionState mode="Custom" customProvider="RedisSessionProvider" timeout="20">
//   <providers>
//     <add name="RedisSessionProvider"
//          type="StackExchange.Redis.Extensions.SessionProvider.RedisSessionStateProvider"
//          connectionString="localhost:6379" />
//   </providers>
// </sessionState>
