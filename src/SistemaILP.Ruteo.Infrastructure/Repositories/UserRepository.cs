using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Domain.Entities;
using SistemaILP.Ruteo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SistemaILP.Ruteo.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByIdAsync(int id) =>
        _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<User?> GetByUserNameOrEmailAsync(string userNameOrEmail) =>
        _context.Users.FirstOrDefaultAsync(u => u.UserName == userNameOrEmail || u.Email == userNameOrEmail);

    public Task<bool> ExistsAsync(string userName, string email) =>
        _context.Users.AnyAsync(u => u.UserName == userName || u.Email == email);

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        user.UpdatedAtUtc = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
