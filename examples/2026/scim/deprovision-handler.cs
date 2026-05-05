// SCIM PATCH/PUT deprovision: set active=false and record the event.
// HTTP DELETE goes to a stricter path that requires an explicit override flag.
app.MapPatch("/scim/v2/Users/{id}", async (
    string id,
    ScimPatchRequest patch,
    IUserStore store,
    IProvisioningLog log,
    CancellationToken ct) =>
{
    var user = await store.GetByScimIdAsync(id, ct);
    if (user is null) return Results.NotFound(ScimError.NotFound(id));

    // Apply patch operations and check whether active changed to false.
    var (updated, wasDeprovisioned) = user.ApplyPatch(patch);

    if (wasDeprovisioned)
    {
        updated = updated with { DeprovisionedAt = DateTimeOffset.UtcNow };
        await log.RecordAsync(new ProvisioningEvent(
            UserId: user.Id,
            EventType: "Deprovisioned",
            Source: patch.ExternalId,
            Payload: patch));
    }

    await store.UpdateAsync(updated, ct);
    return Results.Ok(updated.ToScimResource());
});
