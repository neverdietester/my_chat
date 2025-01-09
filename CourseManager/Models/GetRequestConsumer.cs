using MassTransit;
using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Contracts.Shared.Messages;
using Trainingprogram.Services.Abstractions.CourseManager;

namespace CourseManager.WebAPI.Models
{
    public class GetRequestConsumer : IConsumer<ConsumerCourseDto>
    {
        private readonly ICourseService _courseService;

        public GetRequestConsumer(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task Consume(ConsumeContext<ConsumerCourseDto> context)
        {
            try
            {
                Console.WriteLine($"Received request with ID: {context.Message.Id}");
                var objectId = context.Message.Id;
                var result = await _courseService.AsyncGetCourse(objectId, context.CancellationToken);

                if (result.IsSuccess)
                {
                    Console.WriteLine("Preparing successful response...");
                    await context.RespondAsync(new ConsumerCourseDto
                    {
                        Id = context.Message.Id,
                        Name = result.Data.Name,
                        AuthorId = result.Data.AuthorId.ToString(),
                        Description = result.Data.Description,
                    });
                    Console.WriteLine("Response sent successfully.");
                }
                else
                {
                    Console.WriteLine("Preparing error response: course not found.");
                    await context.RespondAsync(new ErrorResponse
                    {
                        ErrorMessage = "Course not found or another error occurred."
                    });
                    Console.WriteLine("Error response sent.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in consumer: {ex.Message}");
                await context.RespondAsync(new ErrorResponse
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
