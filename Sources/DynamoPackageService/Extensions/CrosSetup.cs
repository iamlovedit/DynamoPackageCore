namespace DynamoPackageService.Extensions;

public static class CrosSetup
{
    private static string PolicyName = "CorsPolicy";

    public static void AddCrosSetup(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName,
                policy => { policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); });
        });
    }

    public static void UseCrosService(this IApplicationBuilder app)
    {
        app.UseCors(PolicyName);
    }
}