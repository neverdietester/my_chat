using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Lesson;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.Result;

namespace CourseManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonCourseController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonCourseController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost("{courseId}/lessons")]
        public async Task<ActionResult<BaseResult<LessonCreateDTO>>> AddLessonToCourse(string courseId, [FromBody] LessonCreateDTO lessonDto)
        {
            var result = await _lessonService.AddLessonToCourse(courseId, lessonDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("lessons/{lessonId}")]
        public async Task<ActionResult<BaseResult<LessonCreateDTO>>> GetLesson(string lessonId)
        {
            var result = await _lessonService.GetLesson(lessonId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{courseId}/lessons")]
        public async Task<ActionResult<BaseResult<List<LessonCreateDTO>>>> GetLessonsByCourse(string courseId)
        {
            var result = await _lessonService.GetLessonsByCourse(courseId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{courseId}/lessons/{lessonId}")]
        public async Task<ActionResult<BaseResult<bool>>> RemoveLessonFromCourse(string courseId, string lessonId)
        {
            var result = await _lessonService.RemoveLessonFromCourse(courseId, lessonId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("lessons/{lessonId}")]
        public async Task<ActionResult<BaseResult<LessonCreateDTO>>> UpdateLesson(string lessonId, [FromBody] LessonUpdateDto lessonDto)
        {
            var result = await _lessonService.UpdateLesson(lessonId, lessonDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
