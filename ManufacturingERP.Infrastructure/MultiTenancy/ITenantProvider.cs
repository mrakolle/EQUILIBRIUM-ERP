namespace ManufacturingERP.Infrastructure.MultiTenancy;

public interface ITenantProvider
{
    string? Schema { get; set; }
    Guid? TenantId { get; set; }
}