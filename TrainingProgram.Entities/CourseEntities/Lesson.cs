using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TrainingProgram.Entities.Enum;
using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.CourseEntities
{
    [MongoDB.EntityFrameworkCore.Collection("Lesson")]
    public class Lesson : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CourseId { get; set; }
        public string? Text { get; set; }
        public List<string>? VideoPath { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
