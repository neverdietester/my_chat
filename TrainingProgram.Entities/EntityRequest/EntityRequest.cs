using TrainingProgram.Entities.IEntity;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Entities.EntityRequest
{
    public class EntityRequest : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public User userId { get; set; }
        public string UserMame { get; set; }

        public string Description { get; set; }

        public bool Approve { get; set; }
    }
}
