﻿namespace Trainingprogram.Contracts.Chat
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public Guid ChatRoomId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
