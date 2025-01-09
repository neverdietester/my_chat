using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RabbitMQ;
using System.Threading;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Contracts.Shared.Messages;
using TrainingProgram.Entities.Result;
using TrainingProgram.WebAPI.Models;

namespace TrainingProgram.WebAPI.Controllers
{
    [Route("rabbit")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private readonly ICourseCommandPublisher _courseCommandPublisher;

        public CreatorController(ICourseCommandPublisher courseCommandPublisher)
        {
            _courseCommandPublisher = courseCommandPublisher;
        }

        [HttpPost("CreateCourse")]
        public ActionResult CreateCourse([FromBody] CourseCreateDTO command)
        {
            try
            {
                _courseCommandPublisher.PublishCreateCourseCommand(command);

                return Ok("Course creation command has been sent.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetCourse/{courseId}")]
        public async Task<ActionResult<BaseResult<ConsumerCourseDto>>> GetCourseAsync(string courseId, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _courseCommandPublisher.PublishGetCourseCommand(courseId, cancellationToken);

                if (result.ErrorMessage != null)
                {
                    return NotFound(result.ErrorMessage);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetCourseFromAuthor/{AuthorId}")]
        public async Task<ActionResult<BaseResult<CourseResponseDTO>>> GetCourseFromAuthorAsync(string AuthorId, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _courseCommandPublisher.PublishGetCourseFromAuthorCommand(AuthorId, cancellationToken);

                if (result.ErrorMessage != null)
                {
                    return NotFound(result.ErrorMessage);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteCourse")]
        public async Task<ActionResult<BaseResult<DeleteCourseDto>>> deleteCourse(DeleteCourseDto courseId, CancellationToken cancellationToken)
        {
            try
            {
                await _courseCommandPublisher.DeleteCourseCommand(courseId, cancellationToken);
                return Ok("CourseDeleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
