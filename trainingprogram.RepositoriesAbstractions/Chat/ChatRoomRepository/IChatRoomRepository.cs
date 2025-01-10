using Trainingprogram.RepositoriesAbstractions.PostgresRepository;
using TrainingProgram.Entities.ChatEntity;

namespace Trainingprogram.RepositoriesAbstractions.Chat.ChatRoomRepository
{
    public interface  IChatRoomRepository : IPostgresRepository<ChatRoom, Guid>
    {
    }
}
