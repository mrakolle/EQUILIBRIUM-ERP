using ManufacturingERP.Domain.Common;

namespace ManufacturingERP.Domain.Entities;
public class Inventory : BaseEntity
{
    public Guid ProductId { get; set; }
    public decimal QuantityOnHand { get; set; }
}