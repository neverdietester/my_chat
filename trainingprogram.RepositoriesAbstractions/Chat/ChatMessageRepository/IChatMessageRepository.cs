using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.RepositoriesAbstractions.PostgresRepository;
using TrainingProgram.Entities.ChatEntity;

namespace Trainingprogram.RepositoriesAbstractions.Chat.ChatMessageRepository
{
    public interface IChatMessageRepository : IPostgresRepository<ChatMessage, Guid>
    {
        Task<ChatMessage> GetByIdAsync(Guid id);
        Task UpdateAsync(ChatMessage message);
    }
}
