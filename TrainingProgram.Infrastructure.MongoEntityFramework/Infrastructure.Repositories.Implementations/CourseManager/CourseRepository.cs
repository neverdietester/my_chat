using MongoDB.Bson;
using Trainingprogram.RepositoriesAbstractions.Courses.CourseRepository;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Infrastructure.MongoCourse.Infrastructure.Repositories.Implementations.CourseManager
{
    public class CourseRepository : RepositoryMongo<Course, ObjectId>, ICourseRepository
    {
        public CourseRepository(DataBaseContextMongo context) : base(context)
        {
        }
    }
}
