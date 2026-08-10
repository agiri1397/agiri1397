using MauiBlazorCleanArchitecture.Application.Interfaces;
using MauiBlazorCleanArchitecture.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MauiBlazorCleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}
