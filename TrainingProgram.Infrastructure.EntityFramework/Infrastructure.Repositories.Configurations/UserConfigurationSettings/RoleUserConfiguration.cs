using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Configurations.UserConfigurationSettings
{
    public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUsers>
    {
        public void Configure(EntityTypeBuilder<RoleUsers> builder)
        {
            builder.HasKey(ru => new { ru.RoleId, ru.UserId });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne<Role>()
                .WithMany()
                .HasForeignKey(ru => ru.RoleId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(ru => ru.UserId);

            // Заполнение данных
            builder.HasData(new List<RoleUsers>
            {
                new RoleUsers
                {
                    UserId = new Guid("1e7249c5-f15e-4f2d-a71a-95e06a5390ea"),
                    RoleId = 1,
                }
            });
        }
    }
}
