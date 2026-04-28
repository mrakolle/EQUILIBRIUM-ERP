using System;
using System.Collections.Generic;
using ManufacturingERP.Domain.Common;
using ManufacturingERP.Domain.Enums;

namespace ManufacturingERP.Domain.Entities;
public class Vendor : BaseEntity
{
    // 1. Basic Identification
    public Guid SupplierId { get; set; } // Primary Key
    public string CompanyName { get; set; } = string.Empty;
    public AccountStatus Status { get; set; } = AccountStatus.Active;

    // 2. Contact & Logistics
    public string PrimaryContact { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhysicalAddress { get; set; } = string.Empty;
    public string ShippingOrigin { get; set; } = string.Empty;

    // 3. Regulatory & Compliance
    public bool IsSdsAvailable { get; set; }
    public string? SdsUrl { get; set; }
    public List<string> Certifications { get; set; } = new();
    public bool ISOCompliant { get; set; } = ComplianceStatus.NonCompliant;
    public RiskTier RiskLevel { get; set; } = RiskTier.Secondary;
    public DateTime? LastAuditDate { get; set; }

    // 4. Commercial Terms
    public string PaymentTerms { get; set; } = "Net 30";
    public int LeadTimeDays { get; set; }
    public decimal MinimumOrderQuantity { get; set; }
    public string CurrencyCode { get; set; } = "ZAR";

    // 5. Technical Details    public List<string> ProductCategories { get; set; } = new();
    public List<string> AvailableGrades { get; set; } = new();
}