﻿using Trainingprogram.RepositoriesAbstractions.Chat.ChatMessageRepository;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatRepository;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatRoomRepository;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatUserRepository;
using Trainingprogram.services.Chat;
using Trainingprogram.Services.Abstractions.ChatMessage;
using TrainingProgram.Infrastructure.PostgresChat;
using TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Implementations.ChatManager;

namespace ChatUsers.WebAPI
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton((IConfigurationRoot)configuration)
                    .InstallServices()
                    .ConfigureContextPostgres(configuration)
                    .InstallRepositories();
            return services;
        }
        private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddHttpContextAccessor()
                .AddTransient<IChatMessageService, ChatMessageService>();

            return serviceCollection;
        }
        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<IChatMessageRepository, ChatMessageRepository>()
                .AddTransient<IUserChatRepository, UserChatRepository>()
                .AddTransient<IChatRoomRepository, ChatRoomRepository>()
                .AddTransient<IUserChatRoomRepository, UserChatRoomRepository>();
            return serviceCollection;
        }

    }
}
