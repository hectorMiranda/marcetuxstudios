// Infrastructure adapter: wraps a SCIM provisioning operation in an OTel activity.
// The domain service itself has no reference to tracing.
using System.Diagnostics;

public static class ScimActivitySource
{
    private static readonly ActivitySource Source = new("AmaWaterways.Identity.Scim", "1.0");

    public static async Task<T> TraceProvisioningAsync<T>(
        string operation,
        string tenantId,
        Func<Task<T>> work)
    {
        using var activity = Source.StartActivity(operation, ActivityKind.Internal);
        activity?.SetTag("scim.tenant_id", tenantId);
        activity?.SetTag("scim.operation", operation);

        try
        {
            var result = await work();
            activity?.SetStatus(ActivityStatusCode.Ok);
            return result;
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity?.RecordException(ex);
            throw;
        }
    }
}
