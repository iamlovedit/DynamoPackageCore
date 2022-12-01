using AutoMapper;
using DynamoPackageService.DTO;
using DynamoPackageService.Infrastructure;
using DynamoPackageService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamoPackageService.Controllers;

public class DynamoPackageController : ApiControllerBase
{
    private readonly ILogger<DynamoPackageController> _logger;
    private readonly IDynamoPackageService _dynamoPackageService;
    private readonly IMapper _mapper;

    public DynamoPackageController(ILogger<DynamoPackageController> logger, IDynamoPackageService dynamoPackageService,
        IMapper mapper)
    {
        _logger = logger;
        _dynamoPackageService = dynamoPackageService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<MessageModel<DynamoPackageDTO>> GetPackageAsync(string id)
    {
        var package = await _dynamoPackageService.GetDynamoPackageByIdAsync(id);
        if (package != null)
        {
            _logger.LogInformation("query packages details by id {id}", id);
            return Success(_mapper.Map<DynamoPackageDTO>(package));
        }

        return Failed<DynamoPackageDTO>("not found");
    }

    [HttpGet("{id}/{version}")]
    public async Task<IActionResult> Download(string id, string version)
    {
        _logger.LogInformation("download package id:{id} version:{version}", id, version);
        return await Task.FromResult(Redirect($"https://dynamopackages.com/download/{id}/{version}"));
    }

    [HttpGet]
    public async Task<MessageModel<PageModel<DynamoPackageDTO>>> GetPackagesByPage(string keyword = "",int pageIndex = 1, int pageSize = 30, string orderField = "")
    {
        _logger.LogInformation("query packages at page:{page} pageSize:{pageSize} keyword:{keyword}",pageIndex,pageSize,keyword);
        var packagesPage = await _dynamoPackageService.QueryPageAsync(
            string.IsNullOrEmpty(keyword) ? null : p => p.Name.Contains(keyword), pageIndex, pageSize,
            string.IsNullOrEmpty(orderField) ? "" : $"{orderField} desc");
        return SucceedPage(packagesPage.ConvertTo<DynamoPackageDTO>(_mapper));
    }

    [HttpGet("stat")]
    public async Task<MessageModel<PackageStatDto>> GetPackageStats(int count = 10)
    {
        var publishPackages =
            await _dynamoPackageService.QueryByOrderAsync(nameof(DynamoPackageDTO.CreateTime), count, true);
        var updatePackages =
            await _dynamoPackageService.QueryByOrderAsync(nameof(DynamoPackageDTO.UpdateTime), count, true);
        var installedPackages =
            await _dynamoPackageService.QueryByOrderAsync(nameof(DynamoPackageDTO.Downloads), count, true);
        var allPackages = await _dynamoPackageService.GetAllAsync();


        var packageStat = new PackageStatDto()
        {
            TotalDownloads = allPackages.Sum(p => p.Downloads),
            TotalPackages = allPackages.Count,
            LastestPublish = _mapper.Map<List<DynamoPackageDTO>>(publishPackages),
            LastestUpdate = _mapper.Map<List<DynamoPackageDTO>>(updatePackages),
            MostDownload = _mapper.Map<List<DynamoPackageDTO>>(installedPackages)
        };
        return Success(packageStat);
    }
}