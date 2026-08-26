using SistemaILP.Ruteo.Application.Interfaces;

namespace SistemaILP.Ruteo.Infrastructure.Persistence;

/// <summary>
/// Crea el archivo SQLite y el esquema si no existen. No siembra datos
/// de ejemplo: los usuarios se crean unicamente a traves de un login
/// exitoso contra el Web Service real. Las excepciones NO se atrapan
/// aqui a proposito: la pantalla de arranque (Splash) es responsable de
/// decidir que hacer si la base de datos no puede abrirse (ver seccion
/// 46 de los requerimientos: no asumir automaticamente "sin sesion").
/// </summary>
public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _context;

    public DbInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.EnsureCreatedAsync();
    }
}
