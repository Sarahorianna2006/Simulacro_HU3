using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webProductos.Domain.Entities;
using webProductos.Domain.Interfaces;
using webProductos.Infrastructure.Data;

//namespace webProductos.Infrastructure.Repositories
//{
    //public class ProductRepository : Repository<Product>, IProductRepository
    //{
       // private readonly AppDbContext _context;

        //public ProductRepository(AppDbContext context) : base(context)
        //{
        //    _context = context;
       // }

        // Obtiene todos los productos que tienen stock disponible (> 0)
        //public async Task<IEnumerable<Product>> GetProductsInStockAsync()
        //{
        //    return await _context.Products
        //        .Where(p => p.Stock > 0)
        //        .ToListAsync();
        //}
 //   }
//}