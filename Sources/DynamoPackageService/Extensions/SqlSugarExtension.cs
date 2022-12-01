using SqlSugar;

namespace DynamoPackageService.Extensions
{
    public static class SqlSugarExtension
    {
        public static void AddSqlSugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var connectionConfig = new ConnectionConfig()
            {
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                ConnectionString = configuration.GetConnectionString("MysqlConnection"),
                InitKeyType = InitKeyType.Attribute
            };
            var sqlsugar=new SqlSugarScope(connectionConfig, client =>
            {
#if DEBUG
                client.Aop.OnLogExecuting = (sql, paras) =>
                {
                  Console.WriteLine(sql);
                };
#endif
            });
            services.AddSingleton<ISqlSugarClient>(sqlsugar);
        }
    }
}
