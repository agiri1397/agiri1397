using MauiBlazorCleanArchitecture.Domain.Entities;

namespace MauiBlazorCleanArchitecture.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);

    Task<User?> GetByUserNameOrEmailAsync(string userNameOrEmail);

    Task<bool> ExistsAsync(string userName, string email);

    Task<User> AddAsync(User user);

    Task UpdateAsync(User user);
}
