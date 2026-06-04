using HoneywellApp.Domain.Entities;
using HoneywellApp.Domain.Interfaces;
using HoneywellApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HoneywellApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) => _db = db;

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        await _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _db.Users.FindAsync([id], ct);

    public async Task<User> AddAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
        return user;
    }

    public async Task UpdateAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsAsync(string email, CancellationToken ct = default) =>
        await _db.Users.AnyAsync(u => u.Email == email, ct);

    public async Task SaveTokenAsync(string email, string token, DateTime expiry, CancellationToken ct = default)
    {
        var user = await GetByEmailAsync(email, ct);
        if (user == null) return;
        user.JwtToken = token;
        user.TokenExpiry = expiry;
        await _db.SaveChangesAsync(ct);
    }
}
