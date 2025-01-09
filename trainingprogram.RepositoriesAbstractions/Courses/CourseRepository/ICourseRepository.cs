using MongoDB.Bson;
using Trainingprogram.RepositoriesAbstractions.Courses.MongoRepository;
using TrainingProgram.Entities.CourseEntities;

namespace Trainingprogram.RepositoriesAbstractions.Courses.CourseRepository
{
    public interface ICourseRepository : IMongoRepository<Course, ObjectId>
    {
    }
}
