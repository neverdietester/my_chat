using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Lesson;
using Trainingprogram.RepositoriesAbstractions.Courses.CourseRepository;
using Trainingprogram.RepositoriesAbstractions.Courses.LessonRepository;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Enum;
using TrainingProgram.Entities.Result;

namespace TrainingProgram.Services.CourseManagerService
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;

        public LessonService(ICourseRepository courseRepository, ILessonRepository lessonRepository)
        {
            _courseRepository = courseRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task<BaseResult<LessonCreateDTO>> AddLessonToCourse(string courseId, LessonCreateDTO lessonDto)
        {
            var course = _courseRepository.Get(ObjectId.Parse(courseId));
            if (course == null)
            {
                return new BaseResult<LessonCreateDTO>
                {
                    ErrorMessage = "Course not found",
                    ErrorCode = (int)ErrorCodes.CourseNotFound
                };
            }

            var lesson = new Lesson
            {
                Id = ObjectId.GenerateNewId(),
                Name = lessonDto.Name,
                Description = lessonDto.Description,
                Text =  lessonDto.Text,
                VideoPath = new List<string>(),
                Questions = new List<Question>(),
                CourseId = ObjectId.Parse(courseId)
            };

            await _lessonRepository.AddAsync(lesson);
            await _lessonRepository.SaveChangesAsync();

            return new BaseResult<LessonCreateDTO>
            {
                Data = new LessonCreateDTO
                {
                    Id = lesson.Id.ToString(),
                    Name = lesson.Name,
                    Description = lesson.Description
                }
            };
        }

        public async Task<BaseResult<LessonCreateDTO>> GetLesson(string lessonId)
        {
            var lesson = _lessonRepository.Get(ObjectId.Parse(lessonId));
            if (lesson == null)
            {
                return new BaseResult<LessonCreateDTO>
                {
                    ErrorMessage = "Lesson not found",
                    ErrorCode = (int)ErrorCodes.LessonNotFound
                };
            }

            return new BaseResult<LessonCreateDTO>
            {
                Data = new LessonCreateDTO
                {
                    Id = lesson.Id.ToString(),
                    Name = lesson.Name,
                    Description = lesson.Description,
                    Text = lesson.Text,
                    VideoPath = lesson.VideoPath
                }
            };
        }

        public async Task<BaseResult<List<LessonCreateDTO>>> GetLessonsByCourse(string courseId)
        {
            var course = _courseRepository.Get(ObjectId.Parse(courseId));
            if (course == null)
            {
                return new BaseResult<List<LessonCreateDTO>>
                {
                    ErrorMessage = "Course not found",
                    ErrorCode = (int)ErrorCodes.CourseNotFound
                };
            }

            var lessonDtos = course.Lessons.Select(lesson => new LessonCreateDTO
            {
                Id = lesson.Id.ToString(),
                Name = lesson.Name,
                Description = lesson.Description,
                Text = lesson.Text,
                VideoPath = lesson.VideoPath
            }).ToList();

            return new BaseResult<List<LessonCreateDTO>> { Data = lessonDtos };
        }

        public async Task<BaseResult<bool>> RemoveLessonFromCourse(string courseId, string lessonId)
        {
            var course = _courseRepository.Get(ObjectId.Parse(courseId));
            if (course == null)
            {
                return new BaseResult<bool>
                {
                    ErrorMessage = "Course not found",
                    ErrorCode = (int)ErrorCodes.CourseNotFound
                };
            }

            var lesson = course.Lessons.FirstOrDefault(l => l.Id == ObjectId.Parse(lessonId));
            if (lesson == null)
            {
                return new BaseResult<bool>
                {
                    ErrorMessage = "Lesson not found in the course",
                    ErrorCode = (int)ErrorCodes.LessonNotFound
                };
            }

            course.Lessons.Remove(lesson);
            await _courseRepository.SaveChangesAsync();

            return new BaseResult<bool> { Data = true };
        }

        public async Task<BaseResult<LessonCreateDTO>> UpdateLesson(string lessonId, LessonUpdateDto lessonDto)
        {
            var lesson = _lessonRepository.Get(ObjectId.Parse(lessonId));
            if (lesson == null)
            {
                return new BaseResult<LessonCreateDTO>
                {
                    ErrorMessage = "Lesson not found",
                    ErrorCode = (int)ErrorCodes.LessonNotFound
                };
            }

            lesson.Name = lessonDto.Name;
            lesson.Description = lessonDto.Description;
            lesson.Text = lessonDto.Text;
            lesson.VideoPath = lessonDto.VideoPath;

            _lessonRepository.Update(lesson);
            await _lessonRepository.SaveChangesAsync();

            return new BaseResult<LessonCreateDTO>
            {
                Data = new LessonCreateDTO
                {
                    Id = lesson.Id.ToString(),
                    Name = lesson.Name,
                    Description = lesson.Description,
                    Text = lesson.Text,
                    VideoPath = lesson.VideoPath
                }
            };
        }
    }
}
