using Trainingprogram.RepositoriesAbstractions.UserRepository;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Implementations.UserManager
{
    public class RoleUsersRepository : RepositoryPostgres<RoleUsers, int>, IRolesUserRepository
    {
        public RoleUsersRepository(DbContextPostgress context) : base(context)
        {
        }
    }
}
