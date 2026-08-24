using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;

namespace SistemaILP.Ruteo.Application.Interfaces;

/// <summary>
/// Cliente del Web Service real de autenticacion. Contrato REAL, tomado
/// de la app Android existente (Retrofit): POST relativo a
/// "validarUsuario". NO cambiar el endpoint ni el metodo HTTP.
/// </summary>
public interface ILoginWebServiceClient
{
    Task<Result<WsLoginResultDto>> LoginAsync(WsLoginRequestDto request, CancellationToken cancellationToken = default);
}
