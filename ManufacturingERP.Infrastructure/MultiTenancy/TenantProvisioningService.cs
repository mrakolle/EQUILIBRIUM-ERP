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
/*
public class TenantProvisioningService
{
    private readonly IConfiguration _config;
    private readonly PublicDbContext _publicDb;

    public TenantProvisioningService(
        IConfiguration config,
        PublicDbContext publicDb)
    {
        _config = config;
        _publicDb = publicDb;
    }

    public async Task<Tenant> CreateTenantAsync(string name)
    {
        var tenantId = Guid.NewGuid();
        var schema = $"tenant_{tenantId:N}";
        var connectionString = _config.GetConnectionString("MasterDb");

        // ----------------------------
        // 1. CREATE SCHEMA
        // ----------------------------
        await using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = $"CREATE SCHEMA IF NOT EXISTS \"{schema}\";";
            await cmd.ExecuteNonQueryAsync();
        }

        // ----------------------------
        // 2. APPLY TENANT MIGRATIONS (FIXED)
        // ----------------------------
        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        await using var tenantContext =
            new TenantDbContext(optionsBuilder.Options, schema);

        // 🔥 CRITICAL FIX: ensure SAME connection + search_path
        await tenantContext.Database.OpenConnectionAsync();

        if (!schema.StartsWith("tenant_"))
            throw new InvalidOperationException("Invalid schema format");

        // safe because schema is system-generated and validated
        #pragma warning disable EF1002
        await tenantContext.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\"");
        #pragma warning restore EF1002

        await tenantContext.Database.MigrateAsync();

        // ----------------------------
        // 3. REGISTER TENANT (PUBLIC)
        // ----------------------------
        var tenant = new Tenant
        {
            Id = tenantId,
            Name = name,
            Schema = schema,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _publicDb.Tenants.Add(tenant);
        await _publicDb.SaveChangesAsync();

        return tenant;
    }
}*/