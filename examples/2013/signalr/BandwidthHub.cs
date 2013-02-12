// A SignalR 1.0 hub. The server pushes new samples to every connected client;
// clients no longer poll. Groups let a browser subscribe to one customer only.
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

public class BandwidthHub : Hub
{
    // Browser calls this to watch a single customer's feed.
    public void Watch(int customerId)
    {
        Groups.Add(Context.ConnectionId, "customer-" + customerId);
    }

    // Server-side code (the log ingester) calls this when a sample arrives.
    public static void Push(int customerId, double gb)
    {
        var ctx = GlobalHost.ConnectionManager.GetHubContext<BandwidthHub>();
        ctx.Clients.Group("customer-" + customerId)
           .sample(new { customerId, gb, at = System.DateTime.UtcNow });
    }
}
