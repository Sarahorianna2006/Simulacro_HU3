using Microsoft.EntityFrameworkCore;
using webProductos.Domain.Entities;
using webProductos.Infrastructure.Data;

namespace webProductos.Application.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    // se obtiene todos los productos registrados
    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    // se obtiene un producto por id
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    
    // se registra un nuevo producto
    public async Task<Product> CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    // se actualiza un producto ya registrado
    public async Task<bool> UpdateProductAsync(Product product)
    {
        var existing = await _context.Products.FindAsync(product.Id);
        if (existing == null)
            return false;

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;

        await _context.SaveChangesAsync();
        return true;
    }

    // se elimina un producto por id
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}