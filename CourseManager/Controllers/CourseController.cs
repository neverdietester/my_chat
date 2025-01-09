
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.Result;


namespace CourseManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost("CreateCourse")]
        public async Task<CourseResponseDTO> CreateCourse([FromBody] CourseCreateDTO courseDto)
        {
            var response = await courseService.AsyncCreateCourse(courseDto);
            return response;
        }

        [HttpGet("GetCourse/{id}")]
        public async Task<ActionResult<BaseResult<CourseResponseDTO>>> GetCourse(string id, CancellationToken cancellationToken)
        {
            var result = await courseService.AsyncGetCourse(id, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getCourseFromAuthor/{id}")]
        public async Task<ActionResult<BaseResult<CourseResponseDTO>>> AsyncGetCourseListFromAuthor(string id, CancellationToken cancellationToken)
        {
            var result = await courseService.AsyncGetCourseListFromAuthor(id, cancellationToken);

            if (result.IsSuccess)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("deleteCourseFromId/{id}")]
        public async Task<ActionResult<BaseResult<CourseResponseDTO>>> DeleteCourseFromId(string id, CancellationToken cancellationToken)
        {
            var result = await courseService.AsyncDeleteCourse(id, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<ActionResult<BaseResult<CourseResponseDTO>>> AsyncUpdateCourse([FromBody] CourseUpdateDTO courseDto)
        {
            var result = await courseService.UpdateCourse(courseDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
