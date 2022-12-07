using DynamoPackageService.Infrastructure;
using Quartz;

namespace DynamoPackageService.Jobs;

public class QueryPackagesJob : IJob
{
    private readonly AppDbContext _appDbContext;
    private readonly IHttpClientFactory _httpClientFactory;

    public QueryPackagesJob(AppDbContext appDbContext, IHttpClientFactory httpClientFactory)
    {
        _appDbContext = appDbContext;
        _httpClientFactory = httpClientFactory;
    }

    public Task Execute(IJobExecutionContext context)
    {
        return Task.Run(() =>
        {
            var httpClient = _httpClientFactory.CreateClient();
            
        });
    }
}