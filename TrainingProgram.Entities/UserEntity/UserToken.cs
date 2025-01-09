using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.UserEntity
{
    public class UserToken : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpireTime { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
