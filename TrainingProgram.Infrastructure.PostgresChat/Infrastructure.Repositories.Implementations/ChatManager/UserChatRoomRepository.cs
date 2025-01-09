using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
