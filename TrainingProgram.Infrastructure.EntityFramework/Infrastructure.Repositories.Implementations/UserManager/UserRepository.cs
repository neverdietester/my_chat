using Trainingprogram.RepositoriesAbstractions.UserRepository;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Implementations.UserManager
{
    public class UserRepository : RepositoryPostgres<User, Guid>, IUserRepository
    {
        public UserRepository(DbContextPostgress context) : base(context)
        {
        }
    }
}
