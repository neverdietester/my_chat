using Microsoft.EntityFrameworkCore;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatUserRepository;
using TrainingProgram.Entities.ChatEntity;

namespace TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Implementations.ChatManager
{
    public class UserChatRepository : RepositoryPostgresChat<UserChat, Guid>, IUserChatRepository
    {
        public UserChatRepository(DbContextPostgressChat context) : base(context)
        {
        }
    }
}
