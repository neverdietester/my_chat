using Trainingprogram.RepositoriesAbstractions.PostgresRepository;
using TrainingProgram.Entities.ChatEntity;

namespace Trainingprogram.RepositoriesAbstractions.Chat.ChatUserRepository
{
    public interface IUserChatRepository : IPostgresRepository<UserChat, Guid>
    {
    }
}
