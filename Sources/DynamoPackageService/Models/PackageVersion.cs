using Newtonsoft.Json;
using SqlSugar;

namespace DynamoPackageService.Models;
public class PackageVersion
{
    [SugarColumn(IsPrimaryKey = true)]
    public string Id { get; set; }

    public string Version { get; set; }

    [SugarColumn(IsNullable = false)]
    public string PackageId { get; set; }

    public string Url { get; set; }

    [JsonProperty("created")]
    public DateTime CreateTime { get; set; }

    [JsonProperty("scan_status")]
    [SugarColumn(IsNullable = true)]
    public string ScanStatus { get; set; }
}