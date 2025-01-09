using Trainingprogram.RepositoriesAbstractions.PostgresRepository;
using TrainingProgram.Entities.UserEntity;

namespace Trainingprogram.RepositoriesAbstractions.UserRepository
{
    public interface IUserRepository : IPostgresRepository<User, Guid>
    {
    }
}
