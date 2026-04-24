using Microsoft.AspNetCore.Mvc;
using ManufacturingERP.Infrastructure.MultiTenancy;
using ManufacturingERP.Domain.Entities;

namespace ManufacturingERP.Api.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantsController : ControllerBase
{
    private readonly TenantProvisioningService _tenantService;

    public TenantsController(TenantProvisioningService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Tenant>> CreateTenant([FromBody] CreateTenantRequest request)
    {
        var tenant = await _tenantService.CreateTenantAsync(request.Name);
        return Ok(tenant);
    }
}

public class CreateTenantRequest
{
    public required string Name { get; set; }
}