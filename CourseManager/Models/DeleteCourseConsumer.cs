using MassTransit;
using Trainingprogram.Contracts.Shared.Messages;
using Trainingprogram.Services.Abstractions.CourseManager;
using TrainingProgram.Services.CourseManagerService;

namespace CourseManager.WebAPI.Models
{
    public class DeleteCourseConsumer : IConsumer<DeleteCourseDto>
    {
        ICourseService courseService;
        public DeleteCourseConsumer(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task Consume(ConsumeContext<DeleteCourseDto> context)
        {
            try
            {
                Console.WriteLine($"Received request with ID: {context.Message.CourseId}");
                var objectId = context.Message.CourseId;
                var result = await courseService.AsyncDeleteCourse(objectId, context.CancellationToken);

                if (result.IsSuccess)
                {
                    Console.WriteLine("Preparing successful response...");
                    await context.RespondAsync($"Deleted Course");
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
