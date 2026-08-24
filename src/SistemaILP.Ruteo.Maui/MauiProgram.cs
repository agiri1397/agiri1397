using SistemaILP.Ruteo.Application;
using SistemaILP.Ruteo.Application.Configuration;
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

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // A proposito NO se inicializa la base de datos aqui de forma
        // bloqueante: si la BD no puede abrirse, este seria el punto en
        // el que la app crashearia antes de mostrar ninguna UI. En vez de
        // eso, Splash.razor la inicializa de forma asincrona al arrancar
        // y maneja el error mostrando un mensaje con reintentar (ver
        // seccion 45/46 de los requerimientos).
        return builder.Build();
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
                // TODO: reemplazar por la Base URL real (Preventa/Autoventa/
                // Despachos comparten la misma por ahora). Debe terminar en "/".
                BaseUrl = "https://pendiente-configurar-base-url.example/api/"
            },
            Database = new DatabaseConfiguration
            {
                DatabaseName = databaseName,
                FullPath = databasePath
            }
        };
    }
}
