using MongoDB.Bson;
using Trainingprogram.RepositoriesAbstractions.Courses.MongoRepository;
using TrainingProgram.Entities.CourseEntities;

namespace Trainingprogram.RepositoriesAbstractions.QuestionRepository
{
    public interface IQuestionRepository : IMongoRepository<Question, ObjectId>
    {
    }
}
