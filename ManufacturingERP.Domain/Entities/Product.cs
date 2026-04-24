using ManufacturingERP.Domain.Common;

namespace ManufacturingERP.Domain.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; } = "";
    public string SKU { get; set; } = "";
    public string Type { get; set; } = "FinishedGoods";
}