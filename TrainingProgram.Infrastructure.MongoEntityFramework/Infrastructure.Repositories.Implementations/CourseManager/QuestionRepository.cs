using MongoDB.Bson;
using Trainingprogram.RepositoriesAbstractions.QuestionRepository;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Infrastructure.MongoCourse.Infrastructure.Repositories.Implementations.CourseManager
{
    public class QuestionRepository : RepositoryMongo<Question, ObjectId>, IQuestionRepository
    {
        public QuestionRepository(DataBaseContextMongo context) : base(context)
        {
        }
    }
}
