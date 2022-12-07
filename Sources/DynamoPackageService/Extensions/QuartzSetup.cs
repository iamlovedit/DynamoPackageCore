using DynamoPackageService.Jobs;
using Quartz;
using System.Configuration;

namespace DynamoPackageService.Extensions;

public static class QuartzSetup
{
    public static void AddQuartzSetup(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.Configure<QuartzOptions>(configuration.GetSection("Quartz"));

        services.AddQuartz(config =>
        {
            config.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = new JobKey(nameof(QueryPackagesJob));
            config.AddJob<QueryPackagesJob>(options =>
            {
                options.WithIdentity(jobKey);
            });

            config.AddTrigger(options =>
            {
                options
                .ForJob(jobKey)
                .WithIdentity("queryPackages_trigger")
                .WithCronSchedule("0/5 * * * * ?"); //0 0 0 1/1 * ? *
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        });
    }
}