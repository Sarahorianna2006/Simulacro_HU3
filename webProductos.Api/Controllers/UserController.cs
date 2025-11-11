using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webProductos.Application.Services;
using webProductos.Domain.Entities;

namespace webProductos.Api.Controllers;

[ApiController]
[Authorize] // Todos los endpoints de este controlador requieren token
[Route("api/[controller]")] // URL base: /api/user
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    // GET: /api/users
    // solo los admin pueden ver todos los usuarios
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetUsersAsync();
        return Ok(users); // Devuelve lista en formato JSON
    }

    // GET: /api/users/{id}
    // los usuarios autenticados pueden ver su informacion
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetUserByIdAsync(id);
        if (user == null)
            return NotFound(new { message = "Usuario no encontrado" });
        return Ok(user);
    }

    // POST: /api/users
    // solo los admin pueden crear usuarios
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        var created = await _service.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: /api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        if (id != user.Id)
            return BadRequest(new { message = "El ID no coincide" });

        var updated = await _service.UpdateUserAsync(user);
        if (!updated)
            return NotFound(new { message = "Usuario no encontrado" });

        return NoContent(); 
    }

    // DELETE: /api/users/{id}
    // solo los admin pueden eliminar
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteUserAsync(id);
        if (!deleted)
            return NotFound(new { message = "Usuario no encontrado" });

        return NoContent();
    }
}