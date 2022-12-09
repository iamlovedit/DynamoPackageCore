﻿using DynamoPackageService.Infrastructure;
using DynamoPackageService.Models;
using DynamoPackageService.UnitOfWork;
using Newtonsoft.Json.Linq;
using Quartz;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace DynamoPackageService.Jobs;

[DisallowConcurrentExecution]
public class QueryPackagesJob : IJob
{
    private readonly AppDbContext _appDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<QueryPackagesJob> _logger;

    public QueryPackagesJob(AppDbContext appDbContext, IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory, ILogger<QueryPackagesJob> logger)
    {
        _appDbContext = appDbContext;
        _unitOfWork = unitOfWork;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var responseMessage = await httpClient.GetAsync("https://dynamopackages.com/");

        if (responseMessage.IsSuccessStatusCode)
        {
            var json = await responseMessage.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                var jObject = JObject.Parse(json);
                var content = jObject["content"];
                if (!string.IsNullOrEmpty(content?.ToString()))
                {
                    var newPacakges = content.ToObject<List<DynamoPackage>>();
                    var client = _appDbContext.GetEntityDB<DynamoPackage>();
                    var oldPackages = await client.GetListAsync();
                    try
                    {
                        _unitOfWork.BeginTransaction();
                        foreach (var package in newPacakges)
                        {
                            if (!oldPackages.Any(p => p.Id == package.Id))
                            {
                                await client.InsertAsync(package);
                            }
                        }
                        _unitOfWork.CommitTransaction();
                    }
                    catch (Exception e)
                    {
                        _unitOfWork.RollbackTransaction();
                        _logger.LogError(e, e.Message);
                    }
                }
            }
        }
    }

}
