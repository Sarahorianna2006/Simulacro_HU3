using Microsoft.AspNetCore.Mvc;
using webProductos.Application.DTOs;
using webProductos.Application.Services;

namespace webProductos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]// URL base: /api/auth
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    // Inyección del servicio de autenticación
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Endpoint para registrar un nuevo usuario.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        try
        {
            var user = await _authService.RegisterAsync(dto);
            return Ok(new
            {
                message = "Usuario registrado con éxito",
                user.Username,
                user.Email,
                user.Role
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Endpoint para iniciar sesión y obtener un token JWT.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        return Ok(new { token });
    }
}