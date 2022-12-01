using DynamoPackageService.Repository;
using DynamoPackageService.Services;

namespace DynamoPackageService.Extensions;

public static class ServicesSetup
{
    public static void AddServicesSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IDynamoPackageService), typeof(Services.DynamoPackageService));
    }
}