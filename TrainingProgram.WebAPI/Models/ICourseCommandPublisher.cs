using MongoDB.Bson;
using RabbitMQ;
using Trainingprogram.Contracts.CourseManager.Course;
using Trainingprogram.Contracts.Shared.Messages;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Result;

namespace TrainingProgram.WebAPI.Models
{
    public interface ICourseCommandPublisher
    {
        Task PublishCreateCourseCommand(CourseCreateDTO command);

        Task<BaseResult<ConsumerCourseDto>> PublishGetCourseCommand(string courseId, CancellationToken cancellationToken);

        Task<BaseResult<CourseAuthorListResponseDTO>> PublishGetCourseFromAuthorCommand(string authorId, CancellationToken cancellationToken);

        Task<BaseResult<DeleteCourseDto>> DeleteCourseCommand(DeleteCourseDto courseId, CancellationToken cancellationToken);
    }
}
