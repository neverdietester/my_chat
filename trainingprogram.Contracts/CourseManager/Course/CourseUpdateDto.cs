using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Lesson;

namespace Trainingprogram.Contracts.CourseManager.Course
{
    public class CourseUpdateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<LessonCreateDTO> Lessons { get; set; } = new List<LessonCreateDTO>();
    }
}
