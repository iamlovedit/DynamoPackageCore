namespace DynamoPackageService.DTO;

public class PackageStatDto
{
    public long TotalDownloads { get; set; }
    
    public long TotalPackages { get; set; }

    public List<DynamoPackageDTO> LastestPublish { get; set; }

    public List<DynamoPackageDTO> LastestUpdate { get; set; }

    public List<DynamoPackageDTO> MostDownload { get; set; }
}