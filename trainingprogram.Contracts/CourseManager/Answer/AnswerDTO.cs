using MongoDB.Bson;

namespace Trainingprogram.Contracts.CourseManager.Answer
{
    public class AnswerDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
