using System.Net.Http.Json;
using System.Text.Json;
using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;
using SistemaILP.Ruteo.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace SistemaILP.Ruteo.Infrastructure.Http;

/// <summary>
/// Migracion literal del endpoint real de Android (Retrofit):
/// POST relativo a "validarUsuario". La Base URL se resuelve en cada
/// llamada via IConnectionSettingsService (no queda fija en
/// HttpClient.BaseAddress) porque el usuario puede cambiarla desde
/// Configuracion sin reiniciar la app. Los mensajes de error mostrados
/// al usuario son siempre comprensibles; el detalle tecnico solo se
/// registra en el log, nunca se expone en la UI.
/// </summary>
public class LoginWebServiceClient : ILoginWebServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IConnectionSettingsService _connectionSettings;
    private readonly ILogger<LoginWebServiceClient> _logger;

    public LoginWebServiceClient(HttpClient httpClient, IConnectionSettingsService connectionSettings, ILogger<LoginWebServiceClient> logger)
    {
        _httpClient = httpClient;
        _connectionSettings = connectionSettings;
        _logger = logger;
    }

    public async Task<Result<WsLoginResultDto>> LoginAsync(WsLoginRequestDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var baseUrl = await _connectionSettings.GetBaseUrlAsync();
            var requestUri = new Uri(new Uri(baseUrl), "validarUsuario");

            var response = await _httpClient.PostAsJsonAsync(requestUri, request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "validarUsuario respondio con status {StatusCode}", response.StatusCode);
                return Result<WsLoginResultDto>.Failure(
                    MessageTexts.Login.ServerUnavailableMessage, MessageTexts.Login.ServerUnavailableTitle);
            }

            var result = await response.Content.ReadFromJsonAsync<WsLoginResultDto>(cancellationToken: cancellationToken);
            if (result is null)
            {
                _logger.LogWarning("validarUsuario devolvio un cuerpo vacio o no interpretable.");
                return Result<WsLoginResultDto>.Failure(
                    MessageTexts.Login.InvalidServerResponseMessage, MessageTexts.Login.InvalidServerResponseTitle);
            }

            return Result<WsLoginResultDto>.Success(result);
        }
        catch (UriFormatException ex)
        {
            _logger.LogError(ex, "Base URL configurada invalida.");
            return Result<WsLoginResultDto>.Failure(
                MessageTexts.Login.InvalidBaseUrlMessage, MessageTexts.Login.InvalidBaseUrlTitle);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Respuesta invalida de validarUsuario.");
            return Result<WsLoginResultDto>.Failure(
                MessageTexts.Login.InvalidResponseMessage, MessageTexts.Login.InvalidResponseTitle);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Timeout llamando a validarUsuario.");
            return Result<WsLoginResultDto>.Failure(
                MessageTexts.Login.TimeoutMessage, MessageTexts.Login.TimeoutTitle);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error de conexion llamando a validarUsuario.");
            return Result<WsLoginResultDto>.Failure(
                MessageTexts.Login.ConnectionErrorMessage, MessageTexts.Login.ConnectionErrorTitle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado llamando a validarUsuario.");
            return Result<WsLoginResultDto>.Failure(
                MessageTexts.Login.UnexpectedErrorMessage, MessageTexts.Login.UnexpectedErrorTitle);
        }
    }
}
