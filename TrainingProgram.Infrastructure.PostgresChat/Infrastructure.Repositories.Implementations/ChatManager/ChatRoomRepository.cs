using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
