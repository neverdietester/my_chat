using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Question;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.Result;

namespace CourseManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("{lessonId}/AddQuestion")]
        public async Task<ActionResult<BaseResult<QuestionDTO>>> AddQuestion(ObjectId lessonId, QuestionDTO questionDto)
        {
            var result = await _questionService.AddQuestionToLesson(lessonId, questionDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<BaseResult<QuestionDTO>>> GetQuestion(ObjectId questionId)
        {
            var result = await _questionService.GetQuestion(questionId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("ByLesson/{lessonId}")]
        public async Task<ActionResult<BaseResult<List<QuestionDTO>>>> GetQuestionsByLesson(ObjectId lessonId)
        {
            var result = await _questionService.GetQuestionsByLesson(lessonId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{lessonId}/{questionId}")]
        public async Task<ActionResult<BaseResult<bool>>> RemoveQuestion(ObjectId lessonId, ObjectId questionId)
        {
            var result = await _questionService.RemoveQuestionFromLesson(lessonId, questionId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{questionId}")]
        public async Task<ActionResult<BaseResult<QuestionDTO>>> UpdateQuestion(ObjectId questionId, QuestionUpdateDTO questionDto)
        {
            var result = await _questionService.UpdateQuestion(questionId, questionDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
