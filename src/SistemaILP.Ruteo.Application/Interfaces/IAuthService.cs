using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;

namespace SistemaILP.Ruteo.Application.Interfaces;

public interface IAuthService
{
    Task<Result<SesionUsuarioDto>> LoginAsync(LoginCredentialsDto credentials);

    /// <summary>
    /// Elimina la fila de la sesion activa en la tabla "usuario" (mismos
    /// datos creados/actualizados durante el login) y limpia el estado
    /// de autenticacion en memoria. Devuelve Failure si la limpieza de
    /// BD falla - el llamador debe mostrar el error, no ocultarlo.
    /// </summary>
    Task<Result> LogoutAsync();

    Task<SesionUsuarioDto?> GetCurrentSessionAsync();
}
