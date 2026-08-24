using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;
using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Domain.Entities;

namespace SistemaILP.Ruteo.Application.Services;

public class AuthService : IAuthService
{
    public const string SessionUserIdKey = "current_user_id";

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISecureStorageService _secureStorage;
    private readonly IAuthStateNotifier _authStateNotifier;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ISecureStorageService secureStorage,
        IAuthStateNotifier authStateNotifier)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _secureStorage = secureStorage;
        _authStateNotifier = authStateNotifier;
    }

    public async Task<Result<CurrentUserDto>> LoginAsync(LoginRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.UserNameOrEmail) || string.IsNullOrWhiteSpace(request.Password))
        {
            return Result<CurrentUserDto>.Failure("Usuario/email y contraseña son obligatorios.");
        }

        var user = await _userRepository.GetByUserNameOrEmailAsync(request.UserNameOrEmail.Trim());
        if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Result<CurrentUserDto>.Failure("Usuario o contraseña incorrectos.");
        }

        await _secureStorage.SetAsync(SessionUserIdKey, user.Id.ToString());
        _authStateNotifier.NotifyAuthenticationStateChanged();

        return Result<CurrentUserDto>.Success(MapToDto(user));
    }

    public async Task<Result<CurrentUserDto>> RegisterAsync(RegisterRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return Result<CurrentUserDto>.Failure("Todos los campos obligatorios deben completarse.");
        }

        if (request.Password.Length < 6)
        {
            return Result<CurrentUserDto>.Failure("La contraseña debe tener al menos 6 caracteres.");
        }

        var exists = await _userRepository.ExistsAsync(request.UserName.Trim(), request.Email.Trim());
        if (exists)
        {
            return Result<CurrentUserDto>.Failure("Ya existe un usuario con ese nombre o correo.");
        }

        var (hash, salt) = _passwordHasher.HashPassword(request.Password);

        var user = new User
        {
            UserName = request.UserName.Trim(),
            Email = request.Email.Trim(),
            DisplayName = string.IsNullOrWhiteSpace(request.DisplayName) ? request.UserName.Trim() : request.DisplayName.Trim(),
            PasswordHash = hash,
            PasswordSalt = salt
        };

        user = await _userRepository.AddAsync(user);

        await _secureStorage.SetAsync(SessionUserIdKey, user.Id.ToString());
        _authStateNotifier.NotifyAuthenticationStateChanged();

        return Result<CurrentUserDto>.Success(MapToDto(user));
    }

    public async Task LogoutAsync()
    {
        _secureStorage.Remove(SessionUserIdKey);
        _authStateNotifier.NotifyAuthenticationStateChanged();
        await Task.CompletedTask;
    }

    public async Task<CurrentUserDto?> GetCurrentUserAsync()
    {
        var idText = await _secureStorage.GetAsync(SessionUserIdKey);
        if (string.IsNullOrEmpty(idText) || !int.TryParse(idText, out var id))
        {
            return null;
        }

        var user = await _userRepository.GetByIdAsync(id);
        return user is null ? null : MapToDto(user);
    }

    private static CurrentUserDto MapToDto(User user) => new()
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        DisplayName = user.DisplayName
    };
}
