using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ManufacturingERP.Infrastructure.MultiTenancy;

namespace ManufacturingERP.Infrastructure.Data;

public class TenantDbContextFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITenantProvider _tenantProvider;

    public TenantDbContextFactory(
        IServiceProvider serviceProvider,
        ITenantProvider tenantProvider)
    {
        _serviceProvider = serviceProvider;
        _tenantProvider = tenantProvider;
    }

    public TenantDbContext Create()
    {
        var schema = _tenantProvider.Schema;

        if (string.IsNullOrWhiteSpace(schema))
            throw new Exception("Tenant schema is not resolved.");

        var options = _serviceProvider
            .GetRequiredService<DbContextOptions<TenantDbContext>>();

        return new TenantDbContext(options, schema);
    }

    // 🔥 KEEP THIS (used during provisioning)
    public TenantDbContext Create(string schema)
    {
        var options = _serviceProvider
            .GetRequiredService<DbContextOptions<TenantDbContext>>();

        return new TenantDbContext(options, schema);
    }
}