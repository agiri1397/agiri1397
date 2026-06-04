using CommunityToolkit.Maui;
using HoneywellApp.Application.Interfaces;
using HoneywellApp.Application.Services;   // IApiAuthClient, IApiProductClient, AuthService, ProductService
using HoneywellApp.Domain.Interfaces;
using HoneywellApp.Infrastructure.Data;
using HoneywellApp.Infrastructure.Repositories;
using HoneywellApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HoneywellApp.MauiBlazor;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Blazor WebView
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // Database
        var dbPath = AppDbContext.GetDbPath();
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"), ServiceLifetime.Singleton);

        // HTTP Clients
        builder.Services.AddHttpClient<ApiAuthClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.example.com/api/v1/");
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        builder.Services.AddHttpClient<ApiProductClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.example.com/api/v1/");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        // Infrastructure
        builder.Services.AddSingleton<ISyncService, SyncService>();
        builder.Services.AddSingleton<IBluetoothPrinterService, BluetoothPrinterService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IApiAuthClient, ApiAuthClient>();
        builder.Services.AddScoped<IApiProductClient, ApiProductClient>();

        // Application
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        var app = builder.Build();

        // Apply EF migrations on startup
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        return app;
    }
}
