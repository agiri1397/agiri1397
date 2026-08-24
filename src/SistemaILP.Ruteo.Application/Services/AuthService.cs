using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;
using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Domain.Entities;

namespace SistemaILP.Ruteo.Application.Services;

/// <summary>
/// Migracion literal de LoginService.java (rama login_remoto +
/// mangeSuccessfullLogin). Autentica contra el Web Service real
/// (POST validarUsuario), y ante un login exitoso crea/actualiza la
/// fila de sesion en la tabla "usuario" - exactamente igual que la app
/// Android existente. El login local/offline de Android (LOGIN_LOCAL)
/// queda fuera de esta etapa.
/// </summary>
public class AuthService : IAuthService
{
    // Igual que en Android: DeviceInfo.getImei() esta temporalmente
    // hardcodeado a este valor mientras no se tengan los dispositivos
    // finales disponibles (ver comentario en LoginService.java). Se
    // replica tal cual, no es una decision de esta migracion.
    private const string TemporaryImei = "000000000000000";

    private readonly ILoginWebServiceClient _loginWebServiceClient;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAuthStateNotifier _authStateNotifier;

    public AuthService(
        ILoginWebServiceClient loginWebServiceClient,
        IUsuarioRepository usuarioRepository,
        IAuthStateNotifier authStateNotifier)
    {
        _loginWebServiceClient = loginWebServiceClient;
        _usuarioRepository = usuarioRepository;
        _authStateNotifier = authStateNotifier;
    }

    public async Task<Result<SesionUsuarioDto>> LoginAsync(LoginCredentialsDto credentials)
    {
        if (string.IsNullOrWhiteSpace(credentials.Usuario) || string.IsNullOrWhiteSpace(credentials.Password))
        {
            return Result<SesionUsuarioDto>.Failure("Usuario y contraseña son obligatorios.");
        }

        var wsRequest = new WsLoginRequestDto
        {
            Usuario = credentials.Usuario.Trim(),
            Password = credentials.Password,
            Imei = TemporaryImei
        };

        var wsResult = await _loginWebServiceClient.LoginAsync(wsRequest);
        if (!wsResult.Succeeded)
        {
            return Result<SesionUsuarioDto>.Failure(wsResult.Error!);
        }

        var response = wsResult.Data!;

        // Igual que Android: el exito lo determina "resultado", no el
        // status HTTP (ver WsResultDTO.SUCCESSFULL_LOGIN = 1).
        if (response.Resultado != 1)
        {
            return Result<SesionUsuarioDto>.Failure(
                string.IsNullOrWhiteSpace(response.Mensaje) ? "Usuario o contraseña incorrectos." : response.Mensaje);
        }

        var configuracion = response.Configuracion;
        if (configuracion is null)
        {
            return Result<SesionUsuarioDto>.Failure("El servidor no devolvió una respuesta válida.");
        }

        await GuardarSesionAsync(wsRequest.Usuario, credentials.Password, configuracion);

        _authStateNotifier.NotifyAuthenticationStateChanged();

        return Result<SesionUsuarioDto>.Success(new SesionUsuarioDto
        {
            AsCodigoUsuario = wsRequest.Usuario,
            Vendedor = configuracion.CodigoVendedor,
            Nombre = configuracion.NombreVendedor
        });
    }

    /// <summary>Migracion literal de LoginService.mangeSuccessfullLogin.</summary>
    private async Task GuardarSesionAsync(string asCodigoUsuario, string password, ConfiguracionDto configuracion)
    {
        var existente = await _usuarioRepository.GetByCodigoUsuarioAsync(asCodigoUsuario);

        if (existente is not null)
        {
            // Igual que Android: NO se actualiza la contraseña de un
            // usuario ya existente en un login exitoso.
            existente.SesionActiva = true;
            existente.Vendedor = configuracion.CodigoVendedor;
            existente.Nombre = configuracion.NombreVendedor;

            await _usuarioRepository.UpsertAsync(existente);
        }
        else
        {
            await _usuarioRepository.UpsertAsync(new Usuario
            {
                AsCodigoUsuario = asCodigoUsuario,
                PassW = password,
                SesionActiva = true,
                Vendedor = configuracion.CodigoVendedor,
                Nombre = configuracion.NombreVendedor
            });
        }
    }

    public async Task<Result> LogoutAsync()
    {
        var sesion = await _usuarioRepository.GetSesionActivaAsync();
        if (sesion is null)
        {
            _authStateNotifier.NotifyAuthenticationStateChanged();
            return Result.Success();
        }

        try
        {
            await _usuarioRepository.DeleteAsync(sesion.AsCodigoUsuario);
        }
        catch (Exception ex)
        {
            return Result.Failure($"No se pudo cerrar la sesión correctamente: {ex.Message}");
        }

        _authStateNotifier.NotifyAuthenticationStateChanged();
        return Result.Success();
    }

    public async Task<SesionUsuarioDto?> GetCurrentSessionAsync()
    {
        var usuario = await _usuarioRepository.GetSesionActivaAsync();
        if (usuario is null)
        {
            return null;
        }

        return new SesionUsuarioDto
        {
            AsCodigoUsuario = usuario.AsCodigoUsuario,
            Vendedor = usuario.Vendedor,
            Nombre = usuario.Nombre
        };
    }
}
