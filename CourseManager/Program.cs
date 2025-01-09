using TrainingProgram.Infrastructure.MongoCourse;

namespace CourseManager.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {

        var host = CreateHostBuilder(args).Build();
        
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContextMongo>();
        }
        
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                });
            });

}
