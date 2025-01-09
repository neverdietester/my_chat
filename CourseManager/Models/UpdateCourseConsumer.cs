using MassTransit;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Services.Abstractions.CourseManager;

namespace CourseManager.WebAPI.Models
{
    public class UpdateCourseConsumer : IConsumer<CourseUpdateDTO>
    {
        ICourseService courseService;
        public UpdateCourseConsumer(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public Task Consume(ConsumeContext<CourseUpdateDTO> context)
        {
            throw new NotImplementedException();
        }
    }
}
