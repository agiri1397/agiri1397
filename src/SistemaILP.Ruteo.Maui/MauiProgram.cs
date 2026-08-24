using SistemaILP.Ruteo.Application;
using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Infrastructure;
using SistemaILP.Ruteo.Maui.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace SistemaILP.Ruteo.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();
        builder.Services.AddAuthorizationCore();

        // SQLite database file lives in the app's private, writable data
        // directory so it survives app restarts and is not accessible to
        // other apps.
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "mauiblazor.app.db3");

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(dbPath);
        builder.Services.AddSingleton<ISecureStorageService, MauiSecureStorageService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            dbInitializer.InitializeAsync().GetAwaiter().GetResult();
        }

        return app;
    }
}
