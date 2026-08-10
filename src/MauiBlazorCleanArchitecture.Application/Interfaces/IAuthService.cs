using MauiBlazorCleanArchitecture.Application.Common;
using MauiBlazorCleanArchitecture.Application.DTOs;

namespace MauiBlazorCleanArchitecture.Application.Interfaces;

public interface IAuthService
{
    Task<Result<CurrentUserDto>> LoginAsync(LoginRequestDto request);

    Task<Result<CurrentUserDto>> RegisterAsync(RegisterRequestDto request);

    Task LogoutAsync();

    Task<CurrentUserDto?> GetCurrentUserAsync();
}
