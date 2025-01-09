using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProgram.Entities.EntityRequest;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Infrastructure.PostgresIdentity.Infrastructure.Repositories.Configurations.UserConfigurationSettings
{
    public class RequestsConfiguration : IEntityTypeConfiguration<EntityRequest>
    {
        public void Configure(EntityTypeBuilder<EntityRequest> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.Id);
        }
    }
}
