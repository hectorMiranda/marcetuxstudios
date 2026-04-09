// Outbox pattern: write the event atomically with the business operation;
// a background worker handles delivery independently.
public class IdentityEventOutbox(DbContext db, IOutboxWorker worker)
{
    public async Task PublishAsync<T>(T domainEvent, CancellationToken ct = default)
        where T : IDomainEvent
    {
        var entry = new OutboxEntry
        {
            Id = Guid.NewGuid(),
            EventType = typeof(T).Name,
            Payload = JsonSerializer.Serialize(domainEvent),
            CreatedAt = DateTimeOffset.UtcNow,
            Status = OutboxStatus.Pending,
        };

        db.Set<OutboxEntry>().Add(entry);
        // Caller commits this as part of their transaction — no separate save here.
    }
}

// Worker (runs on a timer or via IHostedService) picks up Pending entries and delivers.
public record OutboxEntry
{
    public Guid Id { get; init; }
    public string EventType { get; init; } = "";
    public string Payload { get; init; } = "";
    public DateTimeOffset CreatedAt { get; init; }
    public OutboxStatus Status { get; set; }
    public int AttemptCount { get; set; }
    public DateTimeOffset? LastAttemptAt { get; set; }
}
