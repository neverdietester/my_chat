using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Configurations.UserConfigurationSettings
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Login).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.DescriptionBlock).IsRequired(false);

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<RoleUsers>(
                    j => j
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey(r => r.RoleId),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey(u => u.UserId
                    )
            );

            builder.HasData(new List<User>
            {
                new User
                {
                    Id = new Guid("1e7249c5-f15e-4f2d-a71a-95e06a5390ea"),
                    Login = "Admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Password = "FeKw08M4keuw8e9gnsQZQgwg4yDOlMZfvIwzEkSOsiU=",
                    Email = "headshot@mail.ru",
                    BanUser = false,
                    DescriptionBlock = null,
                    IsEmailConfirmed = false,
                }
            });
        }
    }
}
