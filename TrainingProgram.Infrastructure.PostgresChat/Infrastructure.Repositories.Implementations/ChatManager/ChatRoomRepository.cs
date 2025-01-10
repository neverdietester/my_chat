using Microsoft.EntityFrameworkCore;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatRoomRepository;
using TrainingProgram.Entities.ChatEntity;

namespace TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Implementations.ChatManager
{
    public class ChatRoomRepository : RepositoryPostgresChat<ChatRoom, Guid>, IChatRoomRepository
    {
        public ChatRoomRepository(DbContext context) : base(context)
        {
        }
    }
}
