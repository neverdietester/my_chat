using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.RepositoriesAbstractions.PostgresRepository;
using TrainingProgram.Entities.ChatEntity;

namespace Trainingprogram.RepositoriesAbstractions.Chat.ChatUserRepository
{
    public interface IUserChatRepository : IPostgresRepository<UserChat, Guid>
    {
    }
}
