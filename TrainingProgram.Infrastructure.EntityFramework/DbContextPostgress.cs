using Microsoft.EntityFrameworkCore;

namespace TrainingProgram.Infrastructure.PostgresIdentity
{
    public class DbContextPostgress : DbContext
    {
        public DbContextPostgress(DbContextOptions<DbContextPostgress> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextPostgress).Assembly);
        }
    }
}
