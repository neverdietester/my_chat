using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.UserEntity
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public string Email { get; set; }
        ProfileUser Profile { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public UserToken UserToken { get; set; }
        public bool BanUser { get; set; } = false;
        public string? DescriptionBlock { get; set; }
    }
}
