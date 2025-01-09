using Trainingprogram.RepositoriesAbstractions.UserRepository;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Implementations.UserManager
{
    public class RoleRepository : RepositoryPostgres<Role, int>, IRoleRepository
    {
        public RoleRepository(DbContextPostgress context) : base(context)
        {
        }
    }
}
