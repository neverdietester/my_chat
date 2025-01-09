using Microsoft.EntityFrameworkCore;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Infrastructure.MongoCourse
{
    public class DataBaseContextMongo : DbContext
    {
        public DbSet<Answer> Answers { get; init; }
        public DbSet<Course> Courses { get; init; }
        public DbSet<Lesson> lessons { get; init; }
        public DbSet<Question> Questions { get; init; }

        public DataBaseContextMongo(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Answer>();
            modelBuilder.Entity<Question>();


            modelBuilder.Entity<Course>()
               .HasMany(c => c.Lessons)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Lesson>()
                .Property(l => l.CourseId)
                .IsRequired();
        }

    }
}
