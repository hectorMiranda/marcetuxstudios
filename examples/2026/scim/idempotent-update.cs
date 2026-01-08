// Idempotent SCIM User PUT handler — only write if the resource actually changed.
// Returns 200 with the resource if unchanged, same as if it did change, per spec.
app.MapPut("/scim/v2/Users/{id}", async (string id, ScimUser incoming, IUserStore store) =>
{
    var existing = await store.GetByScimIdAsync(id);
    if (existing is null)
        return Results.NotFound(ScimError.NotFound(id));

    if (existing.IsEquivalentTo(incoming))
    {
        // No semantic change — return current resource, emit no events.
        return Results.Ok(existing.ToScimResource());
    }

    var updated = await store.ApplyScimPatchAsync(existing, incoming);
    return Results.Ok(updated.ToScimResource());
})
.WithName("ReplaceUser")
.Produces<ScimUserResource>(200)
.Produces<ScimError>(404);
