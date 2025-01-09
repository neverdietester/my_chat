using MongoDB.Bson;
using Trainingprogram.RepositoriesAbstractions.Courses.MongoRepository;
using TrainingProgram.Entities.CourseEntities;

namespace Trainingprogram.RepositoriesAbstractions.Courses.AnswerRepository
{
    public interface IAnswerRepository : IMongoRepository<Answer, ObjectId>
    {
    }
}
