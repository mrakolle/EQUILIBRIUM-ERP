using ManufacturingERP.Domain.Common;

namespace ManufacturingERP.Domain.Entities;

public class WorkOrder : BaseEntity
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
}