using Trainingprogram.RepositoriesAbstractions.PostgresRepository;
using TrainingProgram.Entities.ChatEntity;

namespace Trainingprogram.RepositoriesAbstractions.Chat.ChatMessageRepository
{
    public interface IChatMessageRepository : IPostgresRepository<ChatMessage, Guid>
    {

    }
}
