namespace ManufacturingERP.Infrastructure.MultiTenancy;

public class TenantProvider : ITenantProvider
{
    public string? Schema { get; set; }
    public Guid? TenantId { get; set; }
}