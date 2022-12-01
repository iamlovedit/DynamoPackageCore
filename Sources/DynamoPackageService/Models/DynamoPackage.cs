using Newtonsoft.Json;
using SqlSugar;

namespace DynamoPackageService.Models
{
    public class DynamoPackage
    {
        [SugarColumn(IsNullable = false)] public string Name { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("created")] public DateTime CreateTime { get; set; }

        [JsonProperty("latest_version_update")]
        public DateTime UpdateTime { get; set; }

        [SugarColumn(IsNullable = true, Length = 10000)]
        public string? License { get; set; }

        [SugarColumn(IsNullable = true)] public string? Engine { get; set; }

        public long Downloads { get; set; }

        public long Votes { get; set; }

        [SugarColumn(IsNullable = true)] public string? Group { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(PackageVersion.PackageId))]
        public List<PackageVersion> Versions { get; set; }

        [Navigate(typeof(PackageMaintainer), nameof(PackageMaintainer.PackageId),
            nameof(PackageMaintainer.MaintainerId))]
        public List<DynamoMaintainer> Maintainers { get; set; }

        [JsonProperty("num_dependents")] public int Dependents { get; set; }

        [SugarColumn(IsNullable = true, Length = 10000)]
        public string? Description { get; set; }
    }
}
