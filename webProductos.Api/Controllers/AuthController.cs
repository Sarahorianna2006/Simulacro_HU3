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
    
    // Endpoint para registrar un nuevo usuario.
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
    
    // Endpoint para iniciar sesión y obtener un token JWT.
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        return Ok(new { token });
    }
    
    // endpoint para refrescar tokens
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        try
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);

            return Ok(new
            {
                token = result.newJwt,
                refreshToken = result.newRefreshToken
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

}