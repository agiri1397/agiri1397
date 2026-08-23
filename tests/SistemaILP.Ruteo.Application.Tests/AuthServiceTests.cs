using SistemaILP.Ruteo.Application.DTOs;
using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Application.Services;
using SistemaILP.Ruteo.Domain.Entities;
using SistemaILP.Ruteo.Infrastructure.Security;
using Xunit;

namespace SistemaILP.Ruteo.Application.Tests;

public class AuthServiceTests
{
    private static AuthService CreateSut(out InMemoryUserRepository userRepository, out InMemorySecureStorage secureStorage)
    {
        userRepository = new InMemoryUserRepository();
        secureStorage = new InMemorySecureStorage();
        return new AuthService(userRepository, new PasswordHasher(), secureStorage, new NoOpAuthStateNotifier());
    }

    [Fact]
    public async Task Register_Then_Login_Succeeds()
    {
        var sut = CreateSut(out _, out _);

        var registerResult = await sut.RegisterAsync(new RegisterRequestDto
        {
            UserName = "alice",
            Email = "alice@example.com",
            DisplayName = "Alice",
            Password = "S3curePass"
        });

        Assert.True(registerResult.Succeeded);

        await sut.LogoutAsync();

        var loginResult = await sut.LoginAsync(new LoginRequestDto
        {
            UserNameOrEmail = "alice",
            Password = "S3curePass"
        });

        Assert.True(loginResult.Succeeded);
        Assert.Equal("alice", loginResult.Data!.UserName);
    }

    [Fact]
    public async Task Login_With_Wrong_Password_Fails()
    {
        var sut = CreateSut(out _, out _);

        await sut.RegisterAsync(new RegisterRequestDto
        {
            UserName = "bob",
            Email = "bob@example.com",
            Password = "CorrectPass1"
        });

        await sut.LogoutAsync();

        var result = await sut.LoginAsync(new LoginRequestDto
        {
            UserNameOrEmail = "bob",
            Password = "WrongPassword"
        });

        Assert.False(result.Succeeded);
    }

    [Fact]
    public async Task Register_With_Duplicate_UserName_Fails()
    {
        var sut = CreateSut(out _, out _);

        await sut.RegisterAsync(new RegisterRequestDto
        {
            UserName = "carol",
            Email = "carol1@example.com",
            Password = "Password1"
        });

        var duplicate = await sut.RegisterAsync(new RegisterRequestDto
        {
            UserName = "carol",
            Email = "carol2@example.com",
            Password = "Password2"
        });

        Assert.False(duplicate.Succeeded);
    }

    private class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public Task<User?> GetByIdAsync(int id) =>
            Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

        public Task<User?> GetByUserNameOrEmailAsync(string userNameOrEmail) =>
            Task.FromResult(_users.FirstOrDefault(u => u.UserName == userNameOrEmail || u.Email == userNameOrEmail));

        public Task<bool> ExistsAsync(string userName, string email) =>
            Task.FromResult(_users.Any(u => u.UserName == userName || u.Email == email));

        public Task<User> AddAsync(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task UpdateAsync(User user) => Task.CompletedTask;
    }

    private class InMemorySecureStorage : ISecureStorageService
    {
        private readonly Dictionary<string, string> _store = new();

        public Task SetAsync(string key, string value)
        {
            _store[key] = value;
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync(string key) =>
            Task.FromResult(_store.TryGetValue(key, out var value) ? value : null);

        public void Remove(string key) => _store.Remove(key);
    }

    private class NoOpAuthStateNotifier : IAuthStateNotifier
    {
        public void NotifyAuthenticationStateChanged()
        {
        }
    }
}
