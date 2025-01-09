using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Contracts.CourseManager.Lesson;
using Trainingprogram.RepositoriesAbstractions.Courses.AnswerRepository;
using Trainingprogram.RepositoriesAbstractions.Courses.CourseRepository;
using Trainingprogram.RepositoriesAbstractions.Courses.LessonRepository;
using Trainingprogram.RepositoriesAbstractions.QuestionRepository;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Enum;
using TrainingProgram.Entities.Result;

namespace TrainingProgram.Services.CourseManagerService
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public CourseService(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor, ILessonRepository lessonRepository, IAnswerRepository answerRepository)
        {
            _courseRepository = courseRepository;
            _HttpContextAccessor = httpContextAccessor;
            _lessonRepository = lessonRepository;
            _answerRepository = answerRepository;
        }
        public async Task<CourseResponseDTO> AsyncCreateCourse(CourseCreateDTO courseDto)
        {
            var course = new Course
            {
                Id = ObjectId.GenerateNewId(),
                Name = courseDto.Name,
                Description = courseDto.Description,
                AuthorId = courseDto.AuthorId,
                Lessons = new List<Lesson>()
            };

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            return new CourseResponseDTO
            {
                Id = course.Id.ToString(),
                Name = course.Name,
                Description = course.Description,
                AuthorId = course.AuthorId.ToString(),
                Lessons = new List<LessonCreateDTO>()
            };
        }

        public async Task<BaseResult<CourseResponseDTO>> AsyncDeleteCourse(string Id, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetAsync(ObjectId.Parse(Id), cancellationToken);
                if (course == null)
                {
                    return new BaseResult<CourseResponseDTO>
                    {
                        ErrorMessage = "Курс не найден.",
                        ErrorCode = (int)ErrorCodes.CourseNotFound
                    };
                }


                foreach (var lesson in course.Lessons)
                {

                    foreach (var question in lesson.Questions)
                    {
                        _answerRepository.Delete(question.Id);
                        _questionRepository.Delete(question);
                    }
                    _lessonRepository.Delete(lesson);
                }

                _courseRepository.Delete(course);
                await _courseRepository.SaveChangesAsync();

                return new BaseResult<CourseResponseDTO>
                {
                    Data = new CourseResponseDTO
                    {
                        Id = course.Id.ToString(),
                        Name = course.Name,
                        Description = course.Description,
                        AuthorId = course.AuthorId.ToString(),
                        Lessons = new List<LessonCreateDTO>() // Уроки не возвращаем
                    },
                    ErrorMessage = "Курс успешно удалён.",
                    ErrorCode = 0
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<CourseResponseDTO>
                {
                    ErrorMessage = "Не удалось удалить курс.",
                    ErrorCode = (int)ErrorCodes.CourseNotDeleted
                };
            }
        }

        public async Task<BaseResult<CourseResponseDTO>> AsyncGetCourse(string id, CancellationToken cancellationToken)
        {
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ObjectId.TryParse(id, out var courseId);
                    var course = _courseRepository.Get(courseId);
                    if (course == null)
                    {
                        return new BaseResult<CourseResponseDTO>
                        {
                            ErrorMessage = "Курс не найден.",
                            ErrorCode = (int)ErrorCodes.CourseNotFounded
                        };
                    }

                    var courseResponseDto = new CourseResponseDTO
                    {
                        Id = course.Id.ToString(),
                        Name = course.Name,
                        Description = course.Description,
                        AuthorId = course.AuthorId.ToString(),
                        Lessons = new List<LessonCreateDTO>() 
                    };

                    return new BaseResult<CourseResponseDTO>
                    {
                        Data = courseResponseDto,
                        ErrorCode = 0
                    };
                }
                catch (OperationCanceledException)
                {
                    return new BaseResult<CourseResponseDTO>
                    {
                        ErrorMessage = "Операция была отменена.",
                        ErrorCode = (int)ErrorCodes.OperationCanceled
                    };
                }
                catch (Exception ex)
                {
                    return new BaseResult<CourseResponseDTO>
                    {
                        ErrorMessage = "Не удалось получить курс.",
                        ErrorCode = (int)ErrorCodes.CourseNotRetrieved
                    };
                }
            }
        }

        public async Task<BaseResult<List<CourseResponseDTO>>> AsyncGetCourseListFromAuthor(string authorId, CancellationToken cancellationToken)
        {
            try
            {
                var allCourses = _courseRepository.GetAll();

                var courses = allCourses.Where(c => c.AuthorId == Guid.Parse(authorId)).ToList();

                var courseResponseDto = courses.Select(course => new CourseResponseDTO
                {
                    Id = course.Id.ToString(),
                    Name = course.Name,
                    Description = course.Description,
                    AuthorId = course.AuthorId.ToString(),
                    Lessons = new List<LessonCreateDTO>()
                }).ToList();

                return new BaseResult<List<CourseResponseDTO>>
                {
                    Data = courseResponseDto,
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<List<CourseResponseDTO>>
                {
                    ErrorMessage = "Не удалось получить список курсов.",
                    ErrorCode = (int)ErrorCodes.CourseListNotRetrieved
                };
            }
        }


        public async Task<BaseResult<CourseResponseDTO>> UpdateCourse(CourseUpdateDTO courseDto)
        {
            try
            {
                var course = _courseRepository.Get(ObjectId.Parse(courseDto.Id));
                if (course == null)
                {
                    return new BaseResult<CourseResponseDTO>
                    {
                        ErrorMessage = "Курс не найден.",
                        ErrorCode = (int)ErrorCodes.CourseNotFounded
                    };
                }

                course.Name = courseDto.Name;
                course.Description = courseDto.Description;


                _courseRepository.Update(course);
                _courseRepository.SaveChanges();

                var courseResponseDto = new CourseResponseDTO
                {
                    Id = course.Id.ToString(),
                    Name = course.Name,
                    Description = course.Description,
                    AuthorId = course.AuthorId.ToString(),
                    Lessons = new List<LessonCreateDTO>()
                };

                return new BaseResult<CourseResponseDTO>
                {
                    Data = courseResponseDto
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<CourseResponseDTO>
                {
                    ErrorMessage = "Не удалось обновить курс.",
                    ErrorCode = (int)ErrorCodes.CourseNotUpdated
                };
            }
        }
    }
}
