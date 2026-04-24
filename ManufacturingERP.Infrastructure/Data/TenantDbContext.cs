using Microsoft.EntityFrameworkCore;
using ManufacturingERP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ManufacturingERP.Infrastructure.Data;

public class TenantDbContext : DbContext
{
    private readonly string _schema;

    public string Schema => _schema;

    public TenantDbContext(DbContextOptions<TenantDbContext> options, string schema)
        : base(options)
    {
        _schema = schema;
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Inventory> Inventory => Set<Inventory>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(_schema);

        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Inventory>().ToTable("Inventory");
        modelBuilder.Entity<WorkOrder>().ToTable("WorkOrders");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .ReplaceService<IModelCacheKeyFactory, TenantModelCacheKeyFactory>()
            .ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}

// 🔥 CACHE KEY FIX (must stay outside DbContext)
internal class TenantModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        var tenantContext = (TenantDbContext)context;
        return (context.GetType(), tenantContext.Schema, designTime);
    }
}