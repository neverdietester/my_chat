using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.UserEntity
{
    class ProfileUser : IEntity<Guid>
    {
        public Guid Id { get; set; }
        User User { get; set; }
        public string? AboutUser { get; set; }
        public DateTime? DateTime { get; set; }
        public List<string>? SoftSkills { get; set; }
        public List<string>? HardSkills { get; set; }
    }
}
