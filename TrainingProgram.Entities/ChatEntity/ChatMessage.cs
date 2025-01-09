using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.ChatEntity
{
    public class ChatMessage : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid SenderId { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTime sentAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; }

        public UserChat user { get; set; } = null!;

        public ChatRoom room { get; set; } = null!;
    }
}