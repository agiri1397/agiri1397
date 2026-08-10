using System.Security.Claims;
using MauiBlazorCleanArchitecture.Application.Interfaces;
using MauiBlazorCleanArchitecture.Application.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace MauiBlazorCleanArchitecture.Infrastructure.Auth;

/// <summary>
/// Blazor AuthenticationStateProvider backed by the locally persisted
/// session (see ISecureStorageService) and the SQLite user store.
/// Registered both as AuthenticationStateProvider and IAuthStateNotifier
/// so the Application layer can trigger a refresh after login/logout
/// without depending on ASP.NET Core Components types.
///
/// Depends directly on IUserRepository/ISecureStorageService (instead of
/// IAuthService) on purpose: AuthService itself depends on
/// IAuthStateNotifier, and depending on IAuthService here would create a
/// circular dependency graph in the DI container.
/// </summary>
public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAuthStateNotifier
{
    private static readonly ClaimsPrincipal Anonymous = new(new ClaimsIdentity());

    private readonly IUserRepository _userRepository;
    private readonly ISecureStorageService _secureStorage;

    public CustomAuthenticationStateProvider(IUserRepository userRepository, ISecureStorageService secureStorage)
    {
        _userRepository = userRepository;
        _secureStorage = secureStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var idText = await _secureStorage.GetAsync(AuthService.SessionUserIdKey);
        if (string.IsNullOrEmpty(idText) || !int.TryParse(idText, out var userId))
        {
            return new AuthenticationState(Anonymous);
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return new AuthenticationState(Anonymous);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("display_name", user.DisplayName)
        };

        var identity = new ClaimsIdentity(claims, authenticationType: "LocalSqlite");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
