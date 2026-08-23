using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Infrastructure.Auth;
using SistemaILP.Ruteo.Infrastructure.Http;
using SistemaILP.Ruteo.Infrastructure.Persistence;
using SistemaILP.Ruteo.Infrastructure.Repositories;
using SistemaILP.Ruteo.Infrastructure.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SistemaILP.Ruteo.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Wires up SQLite persistence, repositories, auth infrastructure and
    /// the sample WS (HTTP) client. <paramref name="dbPath"/> should be an
    /// absolute path inside the app's writable data directory
    /// (e.g. FileSystem.AppDataDirectory in the MAUI head project).
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbPath)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IDbInitializer, DbInitializer>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddScoped<CustomAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<CustomAuthenticationStateProvider>());
        services.AddScoped<IAuthStateNotifier>(sp =>
            sp.GetRequiredService<CustomAuthenticationStateProvider>());

        services.AddHttpClient<IPostsApiService, PostsApiService>(client =>
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            client.Timeout = TimeSpan.FromSeconds(15);
        });

        return services;
    }
}
