using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Answer;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.Result;

namespace CourseManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost("{questionId}/AddAnswer")]
        public async Task<ActionResult<BaseResult<AnswerDTO>>> AddAnswer(ObjectId questionId, AnswerDTO answerDto)
        {
            var result = await _answerService.AddAnswerToQuestion(questionId, answerDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{answerId}")]
        public async Task<ActionResult<BaseResult<AnswerDTO>>> GetAnswer(ObjectId answerId)
        {
            var result = await _answerService.GetAnswer(answerId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("Question/{questionId}")]
        public async Task<ActionResult<BaseResult<List<AnswerDTO>>>> GetAnswersByQuestion(ObjectId questionId)
        {
            var result = await _answerService.GetAnswersByQuestion(questionId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{answerId}")]
        public async Task<ActionResult<BaseResult<AnswerDTO>>> UpdateAnswer(ObjectId answerId, AnswerUpdateDTO answerDto)
        {
            var result = await _answerService.UpdateAnswer(answerId, answerDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{questionId}/RemoveAnswer/{answerId}")]
        public async Task<ActionResult<BaseResult<bool>>> RemoveAnswer(ObjectId questionId, ObjectId answerId)
        {
            var result = await _answerService.RemoveAnswer(questionId, answerId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
