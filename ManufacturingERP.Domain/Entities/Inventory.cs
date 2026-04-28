using System;
using ManufacturingERP.Domain.Common;
using ManufacturingERP.Domain.Enums;

namespace ManufacturingERP.Domain.Entities;

public class Inventory : BaseEntity
{
    //  What type of item this is
    public InventoryItemType ItemType { get; set; }

    // ProductId OR RawMaterialId (depending on type)
    public Guid ItemId { get; set; }

    // Quantity on hand
    public decimal QuantityOnHand { get; set; }

    public string Unit { get; set; } = string.Empty;

    //  CRITICAL for traceability
    public string LotNumber { get; set; } = string.Empty;

    // Where it's stored
    public string Location { get; set; } = string.Empty;

    public DateTime? ExpiryDate { get; set; }
}