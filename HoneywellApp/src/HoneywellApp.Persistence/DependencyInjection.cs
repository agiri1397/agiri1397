using HoneywellApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HoneywellApp.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string dbPath)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));
        return services;
    }
}
