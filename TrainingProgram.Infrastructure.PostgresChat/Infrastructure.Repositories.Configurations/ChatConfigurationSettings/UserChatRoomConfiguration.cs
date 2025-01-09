using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingProgram.Entities.ChatEntity;

namespace TrainingProgram.Infrastructure.PostgresChat.Infrastructure.Repositories.Configurations.ChatConfigurationSettings
{
    public class UserChatRoomConfiguration : IEntityTypeConfiguration<UserChatRoom>
    {
        public void Configure(EntityTypeBuilder<UserChatRoom> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.ChatRoomId });

            builder.Property(uc => uc.joinedAt)
                   .HasDefaultValueSql("NOW()");

            builder.HasOne(uc => uc.user)
                   .WithMany()
                   .HasForeignKey(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uc => uc.chatRoom)
                   .WithMany(r => r.UserChatRooms)
                   .HasForeignKey(uc => uc.ChatRoomId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
