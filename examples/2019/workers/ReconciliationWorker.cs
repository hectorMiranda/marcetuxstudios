// A BackgroundService that consumes messages from Azure Service Bus.
// The generic host provides DI, structured shutdown via stoppingToken,
// and health checks via IHealthCheck registration.
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class ReconciliationWorker : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IReconciliationService _reconciliation;
    private readonly ILogger<ReconciliationWorker> _logger;

    public ReconciliationWorker(
        ServiceBusProcessor processor,
        IReconciliationService reconciliation,
        ILogger<ReconciliationWorker> logger)
    {
        _processor  = processor;
        _reconciliation = reconciliation;
        _logger     = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += HandleMessageAsync;
        _processor.ProcessErrorAsync   += HandleErrorAsync;

        await _processor.StartProcessingAsync(stoppingToken);

        // Block until cancellation is requested (SIGTERM / host shutdown)
        await Task.Delay(Timeout.Infinite, stoppingToken)
                  .ContinueWith(_ => Task.CompletedTask);

        await _processor.StopProcessingAsync();
        _logger.LogInformation("ReconciliationWorker stopped cleanly");
    }

    private async Task HandleMessageAsync(ProcessMessageEventArgs args)
    {
        using var activity = new Activity("reconciliation.process").Start();
        _logger.LogInformation("Processing message {MessageId}", args.Message.MessageId);
        await _reconciliation.ProcessAsync(args.Message.Body.ToObjectFromJson<ReconciliationRequest>());
        await args.CompleteMessageAsync(args.Message);
    }

    private Task HandleErrorAsync(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception, "Service Bus error on {EntityPath}", args.EntityPath);
        return Task.CompletedTask;
    }
}
