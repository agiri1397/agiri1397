using HoneywellApp.Application.DTOs;
using HoneywellApp.Application.Interfaces;
using HoneywellApp.Domain.Entities;
using HoneywellApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace HoneywellApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ISyncService _syncService;
    private readonly ILogger<AuthService> _logger;

    // In a real app this would call the actual API — injected via IApiAuthService
    private readonly IApiAuthClient? _apiAuthClient;

    private string? _currentToken;
    private string? _currentEmail;
    private DateTime? _tokenExpiry;

    public AuthService(
        IUserRepository userRepository,
        ISyncService syncService,
        ILogger<AuthService> logger,
        IApiAuthClient? apiAuthClient = null)
    {
        _userRepository = userRepository;
        _syncService = syncService;
        _logger = logger;
        _apiAuthClient = apiAuthClient;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            bool isOnline = await _syncService.CheckConnectivityAsync(cancellationToken);

            if (isOnline && _apiAuthClient != null)
            {
                return await LoginOnlineAsync(request, cancellationToken);
            }
            else
            {
                return await LoginOfflineAsync(request, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for {Email}", request.Email);
            return LoginResponse.Failed($"Login failed: {ex.Message}");
        }
    }

    private async Task<LoginResponse> LoginOnlineAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var apiResponse = await _apiAuthClient!.AuthenticateAsync(request.Email, request.Password, cancellationToken);

            if (apiResponse == null || !apiResponse.Success || string.IsNullOrEmpty(apiResponse.Token))
            {
                return LoginResponse.Failed(apiResponse?.ErrorMessage ?? "Authentication failed");
            }

            // Cache credentials locally for offline use
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            var passwordHash = HashPassword(request.Password);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    JwtToken = apiResponse.Token,
                    TokenExpiry = apiResponse.TokenExpiry ?? DateTime.UtcNow.AddHours(8),
                    LastLoginAt = DateTime.UtcNow,
                    IsActive = true
                };
                await _userRepository.AddAsync(newUser, cancellationToken);
            }
            else
            {
                existingUser.PasswordHash = passwordHash;
                existingUser.JwtToken = apiResponse.Token;
                existingUser.TokenExpiry = apiResponse.TokenExpiry ?? DateTime.UtcNow.AddHours(8);
                existingUser.LastLoginAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(existingUser, cancellationToken);
            }

            _currentToken = apiResponse.Token;
            _currentEmail = request.Email;
            _tokenExpiry = apiResponse.TokenExpiry ?? DateTime.UtcNow.AddHours(8);

            return LoginResponse.Succeeded(_currentToken, _tokenExpiry.Value, request.Email, offline: false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Online login failed, attempting offline login for {Email}", request.Email);
            return await LoginOfflineAsync(request, cancellationToken);
        }
    }

    private async Task<LoginResponse> LoginOfflineAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null)
        {
            return LoginResponse.Failed("No cached credentials found. Please connect to the internet to log in for the first time.");
        }

        var passwordHash = HashPassword(request.Password);
        if (!string.Equals(user.PasswordHash, passwordHash, StringComparison.Ordinal))
        {
            return LoginResponse.Failed("Invalid email or password.");
        }

        // Use cached token if still valid, otherwise generate a temporary offline token
        string token;
        DateTime expiry;

        if (user.IsTokenValid())
        {
            token = user.JwtToken!;
            expiry = user.TokenExpiry!.Value;
        }
        else
        {
            // Generate a temporary offline session token (not a real JWT — just a placeholder)
            token = GenerateOfflineToken(user.Email);
            expiry = DateTime.UtcNow.AddHours(8);

            await _userRepository.SaveTokenAsync(user.Email, token, expiry, cancellationToken);
        }

        user.LastLoginAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user, cancellationToken);

        _currentToken = token;
        _currentEmail = request.Email;
        _tokenExpiry = expiry;

        return LoginResponse.Succeeded(token, expiry, request.Email, offline: true);
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        _currentToken = null;
        _currentEmail = null;
        _tokenExpiry = null;
        await Task.CompletedTask;
    }

    public async Task<bool> IsAuthenticatedAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return !string.IsNullOrEmpty(_currentToken)
            && _tokenExpiry.HasValue
            && _tokenExpiry.Value > DateTime.UtcNow;
    }

    public async Task<string?> GetCurrentTokenAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return _currentToken;
    }

    public async Task<string?> GetCurrentUserEmailAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return _currentEmail;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "HoneywellSalt2024"));
        return Convert.ToBase64String(bytes);
    }

    private static string GenerateOfflineToken(string email)
    {
        var data = $"{email}:{DateTime.UtcNow.Ticks}:offline";
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
        return $"offline_{Convert.ToBase64String(bytes)}";
    }
}

// Interface for the API authentication client (implemented in Infrastructure)
public interface IApiAuthClient
{
    Task<LoginResponse?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
}
