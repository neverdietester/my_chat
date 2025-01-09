using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.ChatEntity
{
    public class UserChatRoom : IEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTime joinedAt { get; set; } = DateTime.UtcNow;

        public UserChat user { get; set; } = null!;
        public ChatRoom chatRoom { get; set; } = null!;
        public Guid Id { get; set; }
    }
}
