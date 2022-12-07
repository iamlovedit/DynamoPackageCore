using DynamoPackageService.Infrastructure;
using Quartz;

namespace DynamoPackageService.Jobs;

[DisallowConcurrentExecution]
public class QueryPackagesJob : IJob
{
    private readonly AppDbContext _appDbContext;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<QueryPackagesJob> _logger;

    public QueryPackagesJob(AppDbContext appDbContext, IHttpClientFactory httpClientFactory, ILogger<QueryPackagesJob> logger)
    {
        _appDbContext = appDbContext;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await Task.Run(() =>
        {
            
        });
        //return Task.Run(() =>
        //{
        //    //var httpClient = _httpClientFactory.CreateClient();
        //    //var responseMessage = await httpClient.GetAsync("https://dynamopackages.com/");
        //    _logger.LogInformation("HelloWorld");
        //});
    }
}