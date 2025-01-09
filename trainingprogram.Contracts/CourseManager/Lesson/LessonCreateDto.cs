using MongoDB.Bson;
using Trainingprogram.Contracts.CourseManager.Question;
using TrainingProgram.Entities.Enum;

namespace Trainingprogram.Contracts.CourseManager.Lesson
{
    public class LessonCreateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public List<string> VideoPath { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }
}
