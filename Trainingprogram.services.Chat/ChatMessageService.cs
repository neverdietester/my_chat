using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Trainingprogram.Contracts.Chat;
using Trainingprogram.RepositoriesAbstractions.Chat.ChatMessageRepository;
using Trainingprogram.Services.Abstractions.ChatMessage;
using TrainingProgram.Entities.ChatEntity;

namespace Trainingprogram.services.Chat
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository chatMessageRepository;
        private readonly IMapper _mapper;
        public ChatMessageService(IChatMessageRepository chatMessageRepository, IMapper mapper)
        {
            this.chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(ChatMessageDto messageDto)
        {
            var message = _mapper.Map<ChatMessage>(messageDto);
            await chatMessageRepository.AddAsync(message);
        }

        public async Task<IEnumerable<ChatMessageDto>> GetByChatRoomIdAsync(Guid chatRoomId)
        {
            var chatMessages =  chatMessageRepository.GetAll().Where(x => x.ChatRoomId == chatRoomId);
            return _mapper.Map<IEnumerable<ChatMessageDto>>(chatMessages);
        }

        public async Task<ChatMessageDto> GetByIdAsync(Guid id)
        {
            var ChatMessage =  chatMessageRepository.GetAll().Where(x => x.Id == id).FirstOrDefault();
            if(ChatMessage == null)
            {
                throw new Exception("Message not found");
            }
            return _mapper.Map<ChatMessageDto>(ChatMessage);
        }

        //public async Task MarkAsReadAsync(Guid messageId)
        //{
        //    var chatMessage = await chatMessageRepository.GetByIdAsync(messageId);
        //    if (chatMessage == null)
        //    {
        //        throw new Exception("Сообщение не найдено");
        //    }

        //    chatMessage.ReadAt = DateTime.UtcNow;
        //    await chatMessageRepository.UpdateAsync(chatMessage);
        //}

        public async Task SaveAsync()
        {
            await chatMessageRepository.SaveChangesAsync();
        }
    }
}
