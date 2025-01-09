using Trainingprogram.RepositoriesAbstractions.UserRepository;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Implementations.UserManager
{
    public class TokenRepository : RepositoryPostgres<UserToken, Guid>, ITokenRepository
    {
        public TokenRepository(DbContextPostgress context) : base(context)
        {

        }
    }
}
