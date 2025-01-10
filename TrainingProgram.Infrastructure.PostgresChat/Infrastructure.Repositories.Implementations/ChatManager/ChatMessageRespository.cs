using Microsoft.EntityFrameworkCore;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatMessageRepository;
using TrainingProgram.Entities.ChatEntity;

namespace TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Implementations.ChatManager
{
    public class ChatMessageRepository : RepositoryPostgresChat<ChatMessage, Guid>, IChatMessageRepository
    {
        public ChatMessageRepository(DbContext context) : base(context)
        {
        }
    }
}
