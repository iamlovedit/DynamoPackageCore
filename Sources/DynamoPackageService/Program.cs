using DynamoPackageService.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DynamoPackageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            });

            services.AddLogging(loggingBuilder => { loggingBuilder.AddSeq(); });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSqlSugarSetup(configuration);
            services.AddDatabaseSeed();
            services.AddHttpClient();
            services.AddCrosSetup();
            services.AddAutoMapper(typeof(Program));
            services.AddServicesSetup();
            services.AddQuartzSetup(configuration);
            services.AddRedisSetup(configuration);
            services.AddHttpClient();


            var app = builder.Build();
            app.Logger.LogInformation("Adding Routes");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCrosService();

            app.UseAuthorization();


            app.MapControllers();

            app.Logger.LogInformation("Starting the app");
            app.Run();
        }
    }
}