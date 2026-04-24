using Microsoft.EntityFrameworkCore;
using ManufacturingERP.Domain.Entities;

namespace ManufacturingERP.Infrastructure.Data;

public class PublicDbContext : DbContext
{
    public PublicDbContext(DbContextOptions<PublicDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.ToTable("Tenants", "public");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Schema).IsRequired();
            entity.Property(x => x.CreatedAt).IsRequired();
            entity.Property(x => x.IsActive).IsRequired();
        });
    }
}