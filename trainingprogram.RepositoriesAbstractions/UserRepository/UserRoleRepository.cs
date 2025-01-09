using Trainingprogram.RepositoriesAbstractions.PostgresRepository;
using TrainingProgram.Entities.UserEntity;

namespace Trainingprogram.RepositoriesAbstractions.UserRepository
{
    public interface IRolesUserRepository : IPostgresRepository<RoleUsers, int>
    {
    }
}
