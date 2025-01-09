using MassTransit;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Contracts.Shared.Messages;
using Trainingprogram.Services.Abstractions.CourseManager;

namespace CourseManager.WebAPI.Models
{
    public class GetRequestAuthorCourseConsumer : IConsumer<GetCourseRequest>
    {
        private readonly ICourseService _courseService;

        public GetRequestAuthorCourseConsumer(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task Consume(ConsumeContext<GetCourseRequest> context)
        {
            Console.WriteLine($"Received request with ID: {context.Message.AuthorId}");
            var authorId = context.Message.AuthorId;

            var result = await _courseService.AsyncGetCourseListFromAuthor(authorId, context.CancellationToken);

            if (result.IsSuccess && result.Data != null)
            {
                var courses = result.Data.Select(course => new CourseResponseDTO
                {
                    Id = course.Id,
                    Name = course.Name,
                    AuthorId = course.AuthorId,
                    Description = course.Description,
                    Lessons = course.Lessons
                }).ToList();

                await context.RespondAsync(new CourseAuthorListResponseDTO
                {
                    Courses = courses
                });
            }
            else
            {
                await context.RespondAsync(new ErrorResponse
                {
                    ErrorMessage = result.ErrorMessage ?? "No courses found."
                });
            }
        }
    }
}
