using DynamoPackageService.Models;
using DynamoPackageService.Repository;

namespace DynamoPackageService.Services;

public class DynamoPackageService:ServiceBase<DynamoPackage>,IDynamoPackageService
{
    public DynamoPackageService(IRepository<DynamoPackage> dbContext) : base(dbContext)
    {
        
    }

    public async Task<DynamoPackage> GetDynamoPackageByIdAsync(string id)
    {
        return await DAL.DbContext.Queryable<DynamoPackage>()
            .Includes(d => d.Versions)
            .InSingleAsync(id);
    }
}