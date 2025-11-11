using webProductos.Domain.Entities;

namespace webProductos.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsInStockAsync();
}