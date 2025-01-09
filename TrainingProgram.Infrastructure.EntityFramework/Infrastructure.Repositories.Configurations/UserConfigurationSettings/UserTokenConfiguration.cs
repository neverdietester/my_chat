using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Configurations.UserConfigurationSettings
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasOne(ut => ut.User)
                   .WithOne(u => u.UserToken)
                   .HasForeignKey<UserToken>(ut => ut.UserId);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RefreshToken).IsRequired();
            builder.Property(x => x.RefreshTokenExpireTime).IsRequired();

            builder.HasData(new List<UserToken>()
            {
                new UserToken()
                {
                    Id = new Guid("8dbd71b4-b9d6-4518-8013-50528af62a23"),
                    RefreshToken = "waedaweqw321wedqw",
                    RefreshTokenExpireTime = DateTime.UtcNow.AddDays(31),
                    UserId = new Guid("1e7249c5-f15e-4f2d-a71a-95e06a5390ea")
                }
            });
        }
    }
}
