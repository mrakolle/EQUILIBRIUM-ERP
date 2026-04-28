using System;
using ManufacturingERP.Domain.Common;

namespace ManufacturingERP.Domain.Entities;

public class QCResult : BaseEntity
{
    public Guid QCTestId { get; set; }
    public QCTest QCTest { get; set; } = null!;

    public decimal ResultValue { get; set; }

    public bool Passed { get; set; }

    public DateTime TestedAt { get; set; }
}