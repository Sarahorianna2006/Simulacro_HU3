using Microsoft.EntityFrameworkCore;
using webProductos.Domain.Entities;
using webProductos.Infrastructure.Data;

namespace webProductos.Application.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }
    
    // se obtiene todos los usuarios registrados
    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // se obtiene un usuario por su id
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    // se registra un nuevo usuario
    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    // se actualiza un usuario ya registrado
    public async Task<bool> UpdateUserAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null)
            return false;

        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.Role = user.Role;

        await _context.SaveChangesAsync();
        return true;
    }

    // se elimina un usuario por id
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}