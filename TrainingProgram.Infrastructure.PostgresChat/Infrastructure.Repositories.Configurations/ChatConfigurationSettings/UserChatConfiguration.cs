using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProgram.Entities.ChatEntity;

namespace TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Configurations.ChatConfigurationSettings
{
    public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.isOnline)
                .HasDefaultValue(false);

            builder.HasIndex(u => u.isOnline);
        }

    }
}
