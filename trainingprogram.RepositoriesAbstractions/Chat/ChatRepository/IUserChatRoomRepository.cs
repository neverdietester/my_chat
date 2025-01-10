using Trainingprogram.RepositoriesAbstractions.PostgresRepository;
using TrainingProgram.Entities.ChatEntity;

namespace Trainingprogram.RepositoriesAbstractions.Chat.ChatRepository
{
    public interface IUserChatRoomRepository : IPostgresRepository<UserChatRoom, Guid>
    {
    }
}
