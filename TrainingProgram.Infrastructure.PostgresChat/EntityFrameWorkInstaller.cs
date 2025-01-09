using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TrainingProgram.Infrastructure.PostgresChat
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContextPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(name: "PostgressSQL");

            services.AddDbContext<DbContextPostgressChat>(optionsAction: options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;

        }
    }
}
