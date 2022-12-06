using DynamoPackageService.Jobs;
using Quartz;

namespace DynamoPackageService.Extensions;

public static class QuartzSetup
{
    public static void AddQuartzSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

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
                options.ForJob(jobKey).WithIdentity("queryPackages_trigger");
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        });
    }
}