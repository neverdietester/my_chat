using MassTransit;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Services.Abstractions.CourseManager;

namespace CourseManager.WebAPI.Models
{
    public class CreateCourseConsumer : IConsumer<CourseCreateDTO>
    {
        private readonly ICourseService _courseService;

        public CreateCourseConsumer(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task Consume(ConsumeContext<CourseCreateDTO> context)
        {
            var command = context.Message;

             await _courseService.AsyncCreateCourse(command);

        }
    }
}
