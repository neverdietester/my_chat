using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.ChatEntity
{
    public class ChatRoom : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        public ICollection<UserChatRoom> UserChatRooms { get; set; } = new List<UserChatRoom>();
    }
}
