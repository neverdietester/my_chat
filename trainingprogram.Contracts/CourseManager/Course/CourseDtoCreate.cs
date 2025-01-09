namespace Trainingprogram.Contracts.CourseManager.Course
{
    public class CourseCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }
}
