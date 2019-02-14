// Publish a typed, versioned domain event to Azure Service Bus.
// The envelope carries the schema version so consumers can route or skip gracefully.
using Azure.Messaging.ServiceBus;
using System.Text.Json;

public record PaymentInitiatedV1(
    string IdempotencyKey,
    string DebtorAccount,
    string CreditorAccount,
    decimal Amount,
    string Currency,
    DateTimeOffset OccurredAt);

public class PaymentEventPublisher
{
    private readonly ServiceBusSender _sender;
    private const string SchemaVersion = "payment.initiated.v1";

    public PaymentEventPublisher(ServiceBusSender sender) => _sender = sender;

    public async Task PublishAsync(PaymentInitiatedV1 evt, CancellationToken ct = default)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(evt);
        var message = new ServiceBusMessage(body)
        {
            ContentType        = "application/json",
            Subject            = SchemaVersion,
            MessageId          = evt.IdempotencyKey,       // dedup at the bus level
            CorrelationId      = Activity.Current?.Id      // trace link
        };
        message.ApplicationProperties["schemaVersion"] = SchemaVersion;

        await _sender.SendMessageAsync(message, ct);
    }
}
