using MongoDB.Bson;
using Trainingprogram.RepositoriesAbstractions.Courses.LessonRepository;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Infrastructure.MongoCourse.Infrastructure.Repositories.Implementations.CourseManager
{
    public class LessonRepository : RepositoryMongo<Lesson, ObjectId>, ILessonRepository
    {
        public LessonRepository(DataBaseContextMongo context) : base(context)
        {
        }
    }
}
