using SistemaILP.Ruteo.Domain.Entities;

namespace SistemaILP.Ruteo.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByCodigoUsuarioAsync(string asCodigoUsuario);

    /// <summary>
    /// Existe a lo sumo una sesion activa por dispositivo (login hace
    /// upsert, logout elimina la fila) - esto consulta esa fila si
    /// existe, usada tanto para "recuperar sesion" al abrir la app como
    /// por el AuthenticationStateProvider.
    /// </summary>
    Task<Usuario?> GetSesionActivaAsync();

    Task UpsertAsync(Usuario usuario);

    Task DeleteAsync(string asCodigoUsuario);
}
