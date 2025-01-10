using ChatUsers.WebAPI;
using TrainingProgram.Infrastructure.PostgresChat;


public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DbContextPostgressChat>();
        }
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {

                webBuilder.UseStartup<ChatUsers.WebAPI.Startup>();
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                });
            });

}