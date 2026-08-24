using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SistemaILP.Ruteo.Infrastructure.Persistence;

/// <summary>
/// Ensures the SQLite database file and schema exist, and seeds a demo
/// account (demo@mauiblazor.app / Demo123!) so the login screen works
/// immediately after a fresh install, with no backend required.
/// </summary>
public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public DbInitializer(AppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.EnsureCreatedAsync();

        var hasUsers = await _context.Users.AnyAsync();
        if (hasUsers)
        {
            return;
        }

        var (hash, salt) = _passwordHasher.HashPassword("Demo123!");

        _context.Users.Add(new User
        {
            UserName = "demo",
            Email = "demo@mauiblazor.app",
            DisplayName = "Usuario Demo",
            PasswordHash = hash,
            PasswordSalt = salt
        });

        await _context.SaveChangesAsync();
    }
}
