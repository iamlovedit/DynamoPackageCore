using SqlSugar;

namespace DynamoPackageService.Models
{
    public class DynamoPackage
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
