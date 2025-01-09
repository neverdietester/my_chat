using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.CourseEntities
{
    [MongoDB.EntityFrameworkCore.Collection("Question")]
    public class Question : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Text { get; set; }

        [BsonRepresentation(BsonType.ObjectId)] // Определяем внешний ключ для связи с уроком
        public ObjectId LessonId { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
