using System.Reflection;
using System.Text;
using DynamoPackageService.Models;
using Newtonsoft.Json;
using SqlSugar;

namespace DynamoPackageService.Infrastructure;

public class DatabaseSeed
{
    private static string SeedDataFolder = "Seeds";

    public static async Task SeedAsync(AppDbContext appDbContext, string webRootPath)
    {
        if (appDbContext == null)
        {
            throw new ArgumentNullException(nameof(appDbContext));
        }

        if (string.IsNullOrEmpty(webRootPath))
        {
            throw new Exception("获取wwwroot路径时，异常！");
        }
        SeedDataFolder = Path.Combine(webRootPath, SeedDataFolder);
        try
        {
            if (AppDbContext.DbType != DbType.Oracle)
            {
                appDbContext.Database.DbMaintenance.CreateDatabase();
            }
            else
            {
                Console.WriteLine("The Oracle database is not supported");
            }
            Console.WriteLine("Create Tables...");
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = Directory.GetFiles(path, "DynamoPackageService.dll").Select(Assembly.LoadFrom);
            var modelTypes = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(t => t.IsClass && t.Namespace != null && t.Namespace.Equals("DynamoPackageService.Models"));

            foreach (var type in modelTypes)
            {
                if (!appDbContext.Database.DbMaintenance.IsAnyTable(type.Name))
                {
                    Console.WriteLine($"Create Table:{type.Name}");
                    appDbContext.Database.CodeFirst.InitTables(type);
                }
            }
            InitSeed<DynamoPackage>(appDbContext, "DynamoPackages");
            
            InitSeed<PackageVersion>(appDbContext, "DynamoVersions");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private static async void InitSeed<T>(AppDbContext appDbContext, string seedFileName) where T : class, new()
    {
        if (appDbContext == null)
        {
            throw new ArgumentNullException(nameof(appDbContext));
        }

        if (string.IsNullOrEmpty(seedFileName))
        {
            throw new ArgumentNullException(nameof(seedFileName));
        }
        
        if (!await appDbContext.Database.Queryable<T>().AnyAsync())
        {
            Console.WriteLine($"Init table: {typeof(T).Name}");
            var jsonFile = string.Format(SeedDataFolder, seedFileName);
            var json = File.ReadAllText(jsonFile, Encoding.UTF8);
            if (!string.IsNullOrEmpty(json))
            {
                var datas = JsonConvert.DeserializeObject<List<T>>(json);
                await appDbContext.GetEntityDB<T>().InsertRangeAsync(datas);
            }
        }
    }
}