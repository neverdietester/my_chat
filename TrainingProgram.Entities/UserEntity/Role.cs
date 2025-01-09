using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.UserEntity
{
    public class Role : IEntity<int>
    {
        public int Id { get; set; }

        public string role { get; set; }

        public List<User> Users { get; set; }
    }
}
