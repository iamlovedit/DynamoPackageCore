using DynamoPackageService.Infrastructure;
using Quartz;

namespace DynamoPackageService.Jobs;

public class QueryPackagesJob : IJob
{
    private readonly AppDbContext _appDbContext;
    private readonly IHttpClientFactory _httpClient;

    public QueryPackagesJob(AppDbContext appDbContext,IHttpClientFactory httpClient)
    {
        _appDbContext = appDbContext;
        _httpClient = httpClient;
    }
    
    public Task Execute(IJobExecutionContext context)
    {
        return Task.Run(() =>
        {
            
        });
    }
}