using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;
using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Domain.Entities;
using Microsoft.Extensions.Logging;

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
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        ILoginWebServiceClient loginWebServiceClient,
        IUsuarioRepository usuarioRepository,
        IAuthStateNotifier authStateNotifier,
        ILogger<AuthService> logger)
    {
        _loginWebServiceClient = loginWebServiceClient;
        _usuarioRepository = usuarioRepository;
        _authStateNotifier = authStateNotifier;
        _logger = logger;
    }

    public async Task<Result<SesionUsuarioDto>> LoginAsync(LoginCredentialsDto credentials)
    {
        if (string.IsNullOrWhiteSpace(credentials.Usuario) || string.IsNullOrWhiteSpace(credentials.Password))
        {
            return Result<SesionUsuarioDto>.Failure(
                MessageTexts.Login.RequiredFieldsMessage, MessageTexts.Login.RequiredFieldsTitle);
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
            return Result<SesionUsuarioDto>.Failure(wsResult.Error!, wsResult.ErrorTitle, wsResult.MessageType);
        }

        var response = wsResult.Data!;

        // Igual que Android: el exito lo determina "resultado", no el
        // status HTTP (ver WsResultDTO.SUCCESSFULL_LOGIN = 1). El WS no
        // distingue si fallo el usuario o la contraseña, asi que el
        // mensaje se mantiene generico salvo que el propio servidor mande
        // uno especifico en "mensaje" (ver seccion 11 del pedido).
        if (response.Resultado != 1)
        {
            var message = string.IsNullOrWhiteSpace(response.Mensaje)
                ? MessageTexts.Login.InvalidCredentialsMessage
                : response.Mensaje;

            return Result<SesionUsuarioDto>.Failure(message, MessageTexts.Login.InvalidCredentialsTitle);
        }

        var configuracion = response.Configuracion;
        if (configuracion is null)
        {
            return Result<SesionUsuarioDto>.Failure(
                MessageTexts.Login.InvalidServerResponseMessage, MessageTexts.Login.InvalidServerResponseTitle);
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
            _logger.LogError(ex, "Error al eliminar la sesion activa durante logout.");
            return Result.Failure(MessageTexts.Login.LogoutErrorMessage, MessageTexts.Login.LogoutErrorTitle);
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
