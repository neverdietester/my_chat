using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Question;
using TrainingProgram.Entities.Result;

namespace CourseManager.WebAPI.Interfaces
{
    public interface IVideoService
    {
        Task<BaseResult<List<string>>> AddVideoToLesson(ObjectId lessonId, IFormFileCollection questionDto);

        Task<BaseResult<IFormFile?>> GetVideo(ObjectId lessonId, string videoPath);

        Task<BaseResult<bool>> RemoveVideoFromLesson(ObjectId lessonId, string videoPath);
    }
}
