using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Domain.Entities;
using SistemaILP.Ruteo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SistemaILP.Ruteo.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Usuario?> GetByCodigoUsuarioAsync(string asCodigoUsuario) =>
        _context.Usuarios.FirstOrDefaultAsync(u => u.AsCodigoUsuario == asCodigoUsuario);

    public Task<Usuario?> GetSesionActivaAsync() =>
        _context.Usuarios.FirstOrDefaultAsync(u => u.SesionActiva);

    public async Task UpsertAsync(Usuario usuario)
    {
        if (usuario.Id == 0)
        {
            _context.Usuarios.Add(usuario);
        }
        else
        {
            _context.Usuarios.Update(usuario);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string asCodigoUsuario)
    {
        var usuario = await GetByCodigoUsuarioAsync(asCodigoUsuario);
        if (usuario is null)
        {
            return;
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }
}
