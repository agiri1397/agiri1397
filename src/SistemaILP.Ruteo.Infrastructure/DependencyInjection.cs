using SistemaILP.Ruteo.Application.Configuration;
using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Infrastructure.Auth;
using SistemaILP.Ruteo.Infrastructure.Http;
using SistemaILP.Ruteo.Infrastructure.Persistence;
using SistemaILP.Ruteo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SistemaILP.Ruteo.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Wires up SQLite persistence, repositories, auth infrastructure and
    /// el Web Service client real de login. La Base URL y la ruta de la
    /// base de datos vienen de AppConfiguration, construido una sola vez
    /// en el proyecto Maui - Infrastructure no decide variante ni entorno,
    /// solo consume los valores ya resueltos.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={configuration.Database.FullPath}"));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IDbInitializer, DbInitializer>();

        services.AddScoped<CustomAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<CustomAuthenticationStateProvider>());
        services.AddScoped<IAuthStateNotifier>(sp =>
            sp.GetRequiredService<CustomAuthenticationStateProvider>());

        // Sin BaseAddress fija: LoginWebServiceClient resuelve la Base URL
        // en cada llamada via IConnectionSettingsService, porque el
        // usuario puede cambiarla desde Configuracion sin reiniciar la app.
        services.AddHttpClient<ILoginWebServiceClient, LoginWebServiceClient>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }
}
