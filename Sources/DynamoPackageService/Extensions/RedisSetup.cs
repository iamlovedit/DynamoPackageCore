namespace DynamoPackageService.Extensions;

public static class RedisSetup
{
    public static void AddRedisSetup(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        
        
    }
}