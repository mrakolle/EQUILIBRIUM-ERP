using ManufacturingERP.Domain.Common;

namespace ManufacturingERP.Domain.Entities;
public class BOM : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}