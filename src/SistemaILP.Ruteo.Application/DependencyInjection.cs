using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SistemaILP.Ruteo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}
