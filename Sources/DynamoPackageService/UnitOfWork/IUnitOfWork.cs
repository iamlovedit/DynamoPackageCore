using SqlSugar;

namespace DynamoPackageService.UnitOfWork
{
    public interface IUnitOfWork
    {
        SqlSugarScope DbClient { get; }

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
