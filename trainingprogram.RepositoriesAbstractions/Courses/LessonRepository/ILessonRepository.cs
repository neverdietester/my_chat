using MongoDB.Bson;
using Trainingprogram.RepositoriesAbstractions.Courses.MongoRepository;
using TrainingProgram.Entities.CourseEntities;

namespace Trainingprogram.RepositoriesAbstractions.Courses.LessonRepository
{
    public interface ILessonRepository : IMongoRepository<Lesson, ObjectId>
    {
    }
}
