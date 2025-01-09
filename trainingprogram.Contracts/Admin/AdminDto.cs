namespace TrainingProgram.Contracts.Admin
{
    public class AdminDto
    {
        public List<Guid> Guids { get; set; }
        public List<string> Login { get; set; }
        public List<string> Email { get; set; }
        public List<string> FirstName { get; set; }
        public List<string> Roles { get; set; }
        public List<bool> isBan { get; set; }
        public List<string>? BanDescription { get; set; }

    }
}
