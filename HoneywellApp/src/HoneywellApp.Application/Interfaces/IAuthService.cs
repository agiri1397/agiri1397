using HoneywellApp.Application.DTOs;

namespace HoneywellApp.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task LogoutAsync(CancellationToken cancellationToken = default);
    Task<bool> IsAuthenticatedAsync(CancellationToken cancellationToken = default);
    Task<string?> GetCurrentTokenAsync(CancellationToken cancellationToken = default);
    Task<string?> GetCurrentUserEmailAsync(CancellationToken cancellationToken = default);
}
