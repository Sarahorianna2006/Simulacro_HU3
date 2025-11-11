namespace webProductos.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // Contraseña encriptada
    public string Role { get; set; } = "User"; // Rol: puede ser "User" o "Admin"
}