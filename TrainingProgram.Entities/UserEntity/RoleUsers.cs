using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.UserEntity
{
    public class RoleUsers : IEntity<int>
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        public Guid UserId { get; set; }

    }
}
