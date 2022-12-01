using DynamoPackageService.Infrastructure;

namespace DynamoPackageService.Extensions;

public static class DatabaseSetup
{
    public static void AddDatabaseSeed(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        services.AddScoped<DatabaseSeed>();
        services.AddScoped<AppDbContext>();
    }
}