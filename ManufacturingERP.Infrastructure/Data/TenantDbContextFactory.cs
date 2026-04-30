using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ManufacturingERP.Infrastructure.MultiTenancy;

namespace ManufacturingERP.Infrastructure.Data;

public class TenantDbContextFactory
{
    private readonly IConfiguration _configuration;
    private readonly ITenantProvider _tenantProvider;

    public TenantDbContextFactory(
        IConfiguration configuration,
        ITenantProvider tenantProvider)
    {
        _configuration = configuration;
        _tenantProvider = tenantProvider;
    }

    public TenantDbContext Create()
    {
        var schema = _tenantProvider.Schema;

        if (string.IsNullOrWhiteSpace(schema))
            throw new Exception("Tenant schema is not resolved.");

        var options = BuildOptions();

        return new TenantDbContext(options, schema);
    }

    // 🔥 USED DURING TENANT CREATION
    public TenantDbContext Create(string schema)
    {
        var options = BuildOptions();

        return new TenantDbContext(options, schema);
    }

    // 🔥 CENTRALIZED OPTIONS CREATION
    private DbContextOptions<TenantDbContext> BuildOptions()
    {
        var connectionString = _configuration.GetConnectionString("Default");

        return new DbContextOptionsBuilder<TenantDbContext>()
            .UseNpgsql(connectionString)
            .Options;
    }
}