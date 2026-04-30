using ManufacturingERP.Application.Services.Products;
using ManufacturingERP.Domain.Entities;
using ManufacturingERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingERP.Infrastructure.Services.Products;

public class ProductsService : IProductsService
{
    private readonly TenantDbContextFactory _factory;

    public ProductsService(TenantDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<List<Product>> GetAll()
    {
        await using var db = _factory.Create();
        return await db.Products.ToListAsync();
    }
    public async Task<Product> Create(CreateProductRequest request)
    {
        await using var db = _factory.Create();

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            SKU = request.Sku,
            Type = request.Type,
            CreatedAt = DateTime.UtcNow
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();

        return product;
    }
}