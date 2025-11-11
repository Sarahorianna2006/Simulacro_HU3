using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webProductos.Application.Services;
using webProductos.Domain.Entities;

namespace webProductos.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // URL base: /api/product
[Authorize] // Todos los endpoints requieren autenticacion
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    // GET: /api/products
    [Authorize]  // Accesible para cualquier usuario autenticado (Admin o User)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetProductsAsync();
        return Ok(products);
    }

    // GET: /api/products/{id}
    // También accesible por cualquier usuario autenticado
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Producto no encontrado" });
        return Ok(product);
    }

    // POST: /api/products
    // Solo los usuarios con rol "Admin" pueden crear productos
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var created = await _service.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: /api/products/{id}
    // Solo los administradores pueden modificar productos
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest(new { message = "El ID no coincide" });

        var updated = await _service.UpdateProductAsync(product);
        if (!updated)
            return NotFound(new { message = "Producto no encontrado" });

        return NoContent();
    }

    // DELETE: /api/products/{id}
    // Solo los administradores pueden eliminar productos
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteProductAsync(id);
        if (!deleted)
            return NotFound(new { message = "Producto no encontrado" });

        return NoContent();
    }
}