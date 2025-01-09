using CourseManager.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Question;
using Trainingprogram.RepositoriesAbstractions.Courses.LessonRepository;
using Trainingprogram.RepositoriesAbstractions.QuestionRepository;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Result;

namespace CourseManager.WebAPI
{
    public class VideoService : IVideoService
    {
        private readonly ILessonRepository _lessonRepository;

        public VideoService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<BaseResult<List<string>>> AddVideoToLesson(ObjectId lessonId, IFormFileCollection videos)
        {
            var lessonResult = _lessonRepository.Get(lessonId);
            if (lessonResult == null)
            {
                return new BaseResult<List<string>>("Lesson not found", 404, new List<string>());
            }

            var courseId = lessonResult.CourseId;
            string baseDirectory = Path.Combine("Videos", courseId.ToString(), lessonId.ToString());

            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }

            if (lessonResult.VideoPath == null)
                lessonResult.VideoPath = new List<string>();

            var allowedMimeTypes = new List<string> { "video/mp4", "video/x-msvideo", "video/x-matroska", "video/quicktime" }; // Можно добавить другие допустимые MIME-типы видео

            foreach (var video in videos)
            {
                if (!allowedMimeTypes.Contains(video.ContentType))
                {
                    return new BaseResult<List<string>>($"File {video.FileName} is not a valid video file.", 400, new List<string>());
                }

                // Формирование полного пути для видео
                string filePath = Path.Combine(baseDirectory, video.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
                }

                // Сохранить относительный путь к видео
                lessonResult.VideoPath.Add(filePath);
            }

            _lessonRepository.Update(lessonResult);
            await _lessonRepository.SaveChangesAsync();

            return new BaseResult<List<string>>("", 200, lessonResult.VideoPath);
        }

        public async Task<BaseResult<IFormFile?>> GetVideo(ObjectId lessonId, string videoPath)
        {
            var lessonResult = _lessonRepository.Get(lessonId);
            if (lessonResult == null || lessonResult.VideoPath == null || !lessonResult.VideoPath.Contains(videoPath))
            {
                return await Task.FromResult(new BaseResult<IFormFile?>("Video not found", 404, null));
            }

            if (!File.Exists(videoPath))
            {
                return await Task.FromResult(new BaseResult<IFormFile?>("Video file not found", 404, null));
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(videoPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var video = new FormFile(memory, 0, memory.Length, null, Path.GetFileName(videoPath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "video/mp4"
            };

            return new BaseResult<IFormFile?>("", 200, video);
        }


        public async Task<BaseResult<bool>> RemoveVideoFromLesson(ObjectId lessonId, string videoPath)
        {
            var lessonResult = _lessonRepository.Get(lessonId);

            if (lessonResult == null || lessonResult.VideoPath == null || !lessonResult.VideoPath.Contains(videoPath))
            {
                return new BaseResult<bool>("Video not found", 404, false);
            }

            // Удаляем видеофайл из файловой системы
            if (File.Exists(videoPath))
            {
                try
                {
                    File.Delete(videoPath);
                }
                catch (Exception ex)
                {
                    return new BaseResult<bool>($"Error deleting video file: {ex.Message}", 500, false);
                }
            }

            // Удаляем путь видео из списка
            lessonResult.VideoPath.Remove(videoPath);

            // Обновляем урок в репозитории
            _lessonRepository.Update(lessonResult);
            await _lessonRepository.SaveChangesAsync();

            return new BaseResult<bool>("", 200, true);
        }

    }
}