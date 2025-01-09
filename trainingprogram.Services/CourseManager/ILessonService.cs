using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Lesson;
using TrainingProgram.Entities.Result;

namespace Trainingprogram.Services.Abstractions.CourseManager
{
    public interface ILessonService
    {
        public Task<BaseResult<List<LessonCreateDTO>>> GetLessonsByCourse(string courseId);
        public Task<BaseResult<LessonCreateDTO>> GetLesson(string lessonId);
        public Task<BaseResult<bool>> RemoveLessonFromCourse(string courseId, string lessonId);

        public Task<BaseResult<LessonCreateDTO>> UpdateLesson(string lessonId, LessonUpdateDto lessonDto);

        public Task<BaseResult<LessonCreateDTO>> AddLessonToCourse(string courseId, LessonCreateDTO lessonDto);
    }
}
