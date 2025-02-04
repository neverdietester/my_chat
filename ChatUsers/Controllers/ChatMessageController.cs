﻿using Microsoft.AspNetCore.Mvc;
using Trainingprogram.Contracts.Chat;
using Trainingprogram.Services.Abstractions.ChatMessage;

namespace ChatUsers.WebAPI.Controllers
{
    [ApiController]
    [Route("api/chat/messages")]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto messageDto)
        {
            await _chatMessageService.AddAsync(messageDto);
            return Ok();
        }

        [HttpGet("{chatRoomId}")]
        public async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetMessagesByChatRoomId(Guid chatRoomId)
        {
            var messages = await _chatMessageService.GetByChatRoomIdAsync(chatRoomId);
            return Ok(messages);
        }

        //[HttpPut("{id}/read")]
        //public async Task<IActionResult> MarkMessageAsRead(Guid id)
        //{
        //    await _chatMessageService.MarkAsReadAsync(id);
        //    return NoContent();
        //}

        [HttpGet("message/{id}")]
        public async Task<ActionResult<ChatMessageDto>> GetMessageById(Guid id)
        {
            var message = await _chatMessageService.GetByIdAsync(id);
            return Ok(message);
        }
    }
}