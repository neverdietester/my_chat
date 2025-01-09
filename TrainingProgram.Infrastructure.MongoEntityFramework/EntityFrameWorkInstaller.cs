using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TrainingProgram.Infrastructure.MongoCourse
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContextMongo(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("MongoDb");
            var dataBaseNameMongo = configuration["DataBasesName:MongoBases"];

            services.AddDbContext<DataBaseContextMongo>(options =>
            {
                options.UseMongoDB(mongoConnectionString, dataBaseNameMongo)
                       .EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}
