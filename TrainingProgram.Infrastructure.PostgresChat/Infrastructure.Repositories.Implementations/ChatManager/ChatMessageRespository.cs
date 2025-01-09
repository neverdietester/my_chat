using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatMessageRepository;
using TrainingProgram.Entities.ChatEntity;

namespace TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Implementations.ChatManager
{
    public class ChatMessageRespository : RepositoryPostgresChat<ChatMessage, Guid>, IChatMessageRepository
    {
        public ChatMessageRespository(DbContext context) : base(context)
        {
        }
    }
}
