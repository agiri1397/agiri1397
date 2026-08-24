using System.Net.Http.Json;
using System.Text.Json;
using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;
using SistemaILP.Ruteo.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace SistemaILP.Ruteo.Infrastructure.Http;

/// <summary>
/// Migracion literal del endpoint real de Android (Retrofit):
/// POST relativo a "validarUsuario" sobre el HttpClient centralizado
/// (BaseAddress = WebServiceConfiguration.BaseUrl, configurado en
/// DependencyInjection). Los mensajes de error mostrados al usuario son
/// siempre comprensibles; el detalle tecnico solo se registra en el
/// log, nunca se expone en la UI.
/// </summary>
public class LoginWebServiceClient : ILoginWebServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LoginWebServiceClient> _logger;

    public LoginWebServiceClient(HttpClient httpClient, ILogger<LoginWebServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<WsLoginResultDto>> LoginAsync(WsLoginRequestDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("validarUsuario", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "validarUsuario respondio con status {StatusCode}", response.StatusCode);
                return Result<WsLoginResultDto>.Failure(
                    "No fue posible completar el inicio de sesión. Inténtelo nuevamente.");
            }

            var result = await response.Content.ReadFromJsonAsync<WsLoginResultDto>(cancellationToken: cancellationToken);
            if (result is null)
            {
                _logger.LogWarning("validarUsuario devolvio un cuerpo vacio o no interpretable.");
                return Result<WsLoginResultDto>.Failure("El servidor no devolvió una respuesta válida.");
            }

            return Result<WsLoginResultDto>.Success(result);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Respuesta invalida de validarUsuario.");
            return Result<WsLoginResultDto>.Failure("Se recibió una respuesta inválida del servidor.");
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Timeout llamando a validarUsuario.");
            return Result<WsLoginResultDto>.Failure(
                "El servidor tardó demasiado en responder. Inténtelo nuevamente.");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error de conexion llamando a validarUsuario.");
            return Result<WsLoginResultDto>.Failure(
                "No fue posible conectarse con el servidor. Verifique su conexión e inténtelo nuevamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado llamando a validarUsuario.");
            return Result<WsLoginResultDto>.Failure("Ocurrió un error inesperado. Inténtelo nuevamente.");
        }
    }
}
