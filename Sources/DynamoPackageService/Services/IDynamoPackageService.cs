using DynamoPackageService.Models;

namespace DynamoPackageService.Services;

public interface IDynamoPackageService:IServiceBase<DynamoPackage>
{
    Task<DynamoPackage> GetDynamoPackageByIdAsync(string id);
}