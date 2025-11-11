using Microsoft.EntityFrameworkCore;
using webProductos.Domain.Entities;
using webProductos.Domain.Interfaces;
using webProductos.Infrastructure.Data;

namespace webProductos.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}