using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ManufacturingERP.Domain.Entities;
using ManufacturingERP.Infrastructure.Data;

namespace ManufacturingERP.Infrastructure.MultiTenancy;

public class TenantProvisioningService
{
    private readonly TenantDbContextFactory _factory;
    private readonly PublicDbContext _publicDb;

    public TenantProvisioningService(
        TenantDbContextFactory factory,
        PublicDbContext publicDb)
    {
        _factory = factory;
        _publicDb = publicDb;
    }

    public async Task<Tenant> CreateTenantAsync(string tenantName)
    {
        var tenantId = Guid.NewGuid().ToString("N");
        var tenantSchema = $"tenant_{tenantId}";

        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = tenantName,
            Schema = tenantSchema,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _publicDb.Tenants.Add(tenant);
        await _publicDb.SaveChangesAsync();

        await using var context = _factory.Create(tenantSchema);

        await context.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{tenantSchema}\"");

        await context.Database.MigrateAsync();

        return tenant;
    }
}
