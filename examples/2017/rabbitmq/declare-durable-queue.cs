// Declare a durable queue wired to a dead-letter exchange.
// Both the queue and each published message need durable=true/persistent
// or a broker restart silently loses work.
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using var conn = factory.CreateConnection();
using var channel = conn.CreateModel();

// Dead-letter exchange — failed messages land here for inspection.
channel.ExchangeDeclare("dlx.orders", ExchangeType.Direct, durable: true);
channel.QueueDeclare("dlq.orders", durable: true, exclusive: false,
    autoDelete: false, arguments: null);
channel.QueueBind("dlq.orders", "dlx.orders", routingKey: "orders");

// Main queue — x-dead-letter-exchange wires rejects/nacks here.
var args = new System.Collections.Generic.Dictionary<string, object>
{
    ["x-dead-letter-exchange"] = "dlx.orders",
    ["x-message-ttl"]          = 60_000  // optional: expire stale messages
};
channel.QueueDeclare("q.orders.amazon", durable: true, exclusive: false,
    autoDelete: false, arguments: args);

Console.WriteLine("Queue declared. Start your consumers.");
