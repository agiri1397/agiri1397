using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;

namespace SistemaILP.Ruteo.Application.Interfaces;

public interface IAuthService
{
    Task<Result<CurrentUserDto>> LoginAsync(LoginRequestDto request);

    Task<Result<CurrentUserDto>> RegisterAsync(RegisterRequestDto request);

    Task LogoutAsync();

    Task<CurrentUserDto?> GetCurrentUserAsync();
}
