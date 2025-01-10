using Trainingprogram.RepositoriesAbstractions.Chat.ChatMessageRepository;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatRepository;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatRoomRepository;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatUserRepository;
using Trainingprogram.RepositoriesAbstractions.UserRepository;
using Trainingprogram.services.Chat;
using Trainingprogram.Services.Abstractions.Admin;
using Trainingprogram.Services.Abstractions.ChatMessage;
using Trainingprogram.Services.Abstractions.Token;
using Trainingprogram.Services.Abstractions.User;
using TrainingProgram.Entities.Settings;
using TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Implementations.ChatManager;
using TrainingProgram.Infrastructure.PostgresIdentity;
using TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Implementations.UserManager;
using TrainingProgram.services.Administration;
using TrainingProgram.Services.OAuth;
using TrainingProgram.WebAPI.Models;

namespace TrainingProgram.WebAPI
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton((IConfigurationRoot)configuration)
                    .InstallServices()
                    .ConfigureContextPostgres(configuration)
                    .Configure<JwtSettings>(configuration.GetSection(JwtSettings.DefaultSection))
                    .InstallRepositories();
            return services;
        }
        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
            .AddHttpContextAccessor()
            .AddTransient<ICourseCommandPublisher, CourseCommandPublisher>()
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<IUserService, UserServices>()
            .AddTransient<IAdminService, AdminService>()
            .AddTransient<IChatMessageService, ChatMessageService>(); 



            return serviceCollection;
        }
        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ITokenRepository, TokenRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IRoleRepository, RoleRepository>()
                .AddTransient<IRolesUserRepository, RoleUsersRepository>();
   



            return serviceCollection;
        }

    }
}
