using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Configurations.UserConfigurationSettings
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.role).IsRequired();

            builder.HasData(new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    role = "Admin"
                },
                new Role() {
                    Id = 2,
                    role = "Moderator"
                },
                new Role()
                {
                    Id = 3,
                    role = "User"
                },
               new Role()
               {
                   Id= 4,
                   role = "CreatorUser"
               }
            });
        }
    }
}
