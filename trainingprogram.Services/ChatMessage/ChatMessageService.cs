using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.Chat;

namespace Trainingprogram.Services.Abstractions.ChatMessage
{
    public interface IChatMessageService
    {
        Task<ChatMessageDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ChatMessageDto>> GetByChatRoomIdAsync(Guid chatRoomId);
        Task AddAsync(ChatMessageDto message);
        Task SaveAsync();
        Task MarkAsReadAsync(Guid messageId);
    }
}
