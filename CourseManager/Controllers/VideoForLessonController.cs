using CourseManager.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.IO.Compression;
using System.Security.AccessControl;
using Trainingprogram.Contracts.CourseManager.Lesson;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Result;

namespace CourseManager.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VideoForLessonController : Controller
    {
        private readonly IVideoService _videoService;

        public VideoForLessonController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpPost("{lessonsId}")]
        public async Task<ActionResult<BaseResult<string>>> AddVideoToLesson(ObjectId lessonsId, [FromForm] IFormFileCollection videoFiles)
        {
            var result = await _videoService.AddVideoToLesson(lessonsId, videoFiles);
            if(result.ErrorCode == 200)
                return Ok();
            else 
                return BadRequest(result);
        }

        [HttpGet("get-video/{lessonsId}/{videoPath}")]
        public async Task<ActionResult> GetVideoFromLesson(ObjectId lessonsId, string videoPath)
        {
            var lessonResult = await _videoService.GetVideo(lessonsId, videoPath);
            if (lessonResult.ErrorCode != 200 || lessonResult.Data == null)
            {
                return NotFound("Video file not found");
            }

            var videoFile = lessonResult.Data as IFormFile; // Убедитесь, что это корректный тип.
            if (videoFile == null)
            {
                return NotFound("Video file not found");
            }

            var stream = videoFile.OpenReadStream();
            return File(stream, "video/mp4", Path.GetFileName(videoPath));
        }



        [HttpDelete("remove-video/{lessonId}")]
        public async Task<IActionResult> RemoveVideoFromLesson(ObjectId lessonId, [FromQuery] string videoPath)
        {
            var result = await _videoService.RemoveVideoFromLesson(lessonId, videoPath);
            if (result.ErrorCode == 200)
            {
                return Ok(result.Data);
            }
            return StatusCode((int)result.ErrorCode, result.ErrorMessage);
        }
    }
}
