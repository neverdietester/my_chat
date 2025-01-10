using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProgram.Entities.ChatEntity;

namespace TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Configurations.ChatConfigurationSettings
{
    public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(r => r.Name).IsUnique();
            builder.Property(r => r.Created)
                .HasDefaultValue("Now()");
        }
    }
}
