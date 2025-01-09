using TrainingProgram.Entities.Enum;

namespace Trainingprogram.Contracts.CourseManager.Lesson
{
    public class LessonUpdateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Text { get; set; }       // Только для текстовых уроков
        public List<string>? VideoPath { get; set; }   // Только для видео-уроков
    }
}
