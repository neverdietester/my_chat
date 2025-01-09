using MongoDB.Bson;
using Trainingprogram.RepositoriesAbstractions.Courses.AnswerRepository;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Infrastructure.MongoCourse.Infrastructure.Repositories.Implementations.CourseManager
{
    public class AnswerRepository : RepositoryMongo<Answer, ObjectId>, IAnswerRepository
    {
        public AnswerRepository(DataBaseContextMongo context) : base(context)
        {
        }
    }
}
