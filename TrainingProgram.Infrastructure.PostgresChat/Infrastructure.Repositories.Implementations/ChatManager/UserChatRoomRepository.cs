using Microsoft.EntityFrameworkCore;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatRepository;
using TrainingProgram.Entities.ChatEntity;

namespace TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Implementations.ChatManager
{
    public class UserChatRoomRepository : RepositoryPostgresChat<UserChatRoom, Guid>, IUserChatRoomRepository
    {
        public UserChatRoomRepository(DbContext context) : base(context)
        {
        }
    }
}
