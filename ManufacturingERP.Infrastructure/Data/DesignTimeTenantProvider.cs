using ManufacturingERP.Infrastructure.MultiTenancy;

namespace ManufacturingERP.Infrastructure.Data;

public class DesignTimeTenantProvider : ITenantProvider
{
    public Guid? TenantId { get; set; } = Guid.Empty;

    // Design-time schema placeholder
    public string? Schema { get; set; } = "public";
}