using ManufacturingERP.Domain.Entities;
namespace ManufacturingERP.Application.Services.Products;public interface IProductsService
{
    Task<List<Product>> GetAll();
    Task<Product> Create(CreateProductRequest request);
}