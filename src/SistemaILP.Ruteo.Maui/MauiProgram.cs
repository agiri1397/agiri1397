using SistemaILP.Ruteo.Application;
using SistemaILP.Ruteo.Application.Configuration;
using SistemaILP.Ruteo.Application.Services;
using SistemaILP.Ruteo.Infrastructure;
using SistemaILP.Ruteo.Maui.Services;
using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.UI.Services;
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

        var appConfiguration = BuildAppConfiguration();
        builder.Services.AddSingleton(appConfiguration);

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(appConfiguration);
        builder.Services.AddSingleton<IPreferencesService, MauiPreferencesService>();
        builder.Services.AddSingleton<ThemeState>();
        builder.Services.AddSingleton<DatabaseStartupState>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // El esquema DEBE existir antes de que se renderice cualquier
        // componente Razor: CascadingAuthenticationState consulta la
        // tabla "usuario" (via CustomAuthenticationStateProvider) apenas
        // arranca el arbol de componentes, sin esperar a que Splash.razor
        // termine su propia inicializacion - si se hace de forma
        // asincrona/diferida, hay una condicion de carrera real. Por eso
        // se hace aqui, bloqueante, ANTES de devolver la app. Si falla, NO
        // se relanza la excepcion (eso crashearia la app antes de mostrar
        // cualquier UI): se guarda el resultado en DatabaseStartupState y
        // Splash.razor decide que mostrar (ver seccion 45/46).
        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            var startupState = scope.ServiceProvider.GetRequiredService<DatabaseStartupState>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<App>>();

            try
            {
                dbInitializer.InitializeAsync().GetAwaiter().GetResult();
                startupState.Succeeded = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fallo al inicializar la base de datos local durante el arranque.");
                startupState.Succeeded = false;
            }
        }

        return app;
    }

    private static AppVariant ResolveVariant()
    {
        // Unico lugar de conditional compilation para la variante - el
        // property AppVariant del csproj (VARIANT_PREVENTA / VARIANT_AUTOVENTA
        // / VARIANT_DESPACHOS) se resuelve aqui, una sola vez, y de ahi en
        // adelante todo el resto de la app trabaja con AppConfiguration.Variant.
#if VARIANT_AUTOVENTA
        return AppVariant.Autoventa;
#elif VARIANT_DESPACHOS
        return AppVariant.Despachos;
#else
        return AppVariant.Preventa;
#endif
    }

    private static AppConfiguration BuildAppConfiguration()
    {
        var variant = ResolveVariant();

        var databaseName = "aurora.db3";
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);

        return new AppConfiguration
        {
            Variant = variant,
            DisplayVersion = AppInfo.Current.VersionString,
            BuildNumber = AppInfo.Current.BuildString,
            WebService = new WebServiceConfiguration
            {
                // Base URL de pruebas (Preventa/Autoventa/Despachos comparten
                // la misma por ahora). Cuando exista la de producción, hay
                // que decidir cómo distinguir Debug/Release u otra estrategia
                // - por ahora es única para toda la app.
                BaseUrl = "https://pruebas.ilpsa.com:8088/WS/Preventaapigtpruebas/apigt/appmovil/"
            },
            Database = new DatabaseConfiguration
            {
                DatabaseName = databaseName,
                FullPath = databasePath
            },
            About = new AppInfoConfiguration
            {
                Developer = "ILP",
                SupportEmail = "sop-app@ilpsa.com",
                SupportPhone = "+502 2420 0323"
            }
        };
    }
}
