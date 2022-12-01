using Newtonsoft.Json;
using SqlSugar;

namespace DynamoPackageService.Models;

public class DynamoMaintainer
{
    [JsonProperty("_id")]
    public string Id { get; set; }

    [SugarColumn(IsNullable = false)]
    public string Username { get; set; }

    [Navigate(typeof(PackageMaintainer), nameof(PackageMaintainer.MaintainerId), nameof(PackageMaintainer.PackageId))]
    public List<DynamoPackage> Packages { get; set; }
}