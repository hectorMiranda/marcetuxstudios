// Tenant-scoped repository base: all queries automatically filter to the current tenant.
// Override QueryAll() explicitly and deliberately if cross-tenant reads are needed.
public abstract class TenantScopedRepository<T>(
    DbContext db,
    ITenantContext tenantContext)
    where T : class, ITenantEntity
{
    protected IQueryable<T> Query() =>
        db.Set<T>().Where(e => e.TenantId == tenantContext.TenantId);

    // Explicit opt-out: name makes the cross-tenant access obvious in code review.
    protected IQueryable<T> QueryAllTenants() => db.Set<T>();

    protected async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await Query().FirstOrDefaultAsync(e => e.Id == id, ct);

    protected async Task<List<T>> ListAsync(CancellationToken ct = default) =>
        await Query().ToListAsync(ct);
}
