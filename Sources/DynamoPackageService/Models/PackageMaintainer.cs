using SqlSugar;

namespace DynamoPackageService.Models;

public class PackageMaintainer
{
    [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
    public string PackageId { get; set; }

    [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
    public string MaintainerId { get; set; }
}