using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.CourseEntities
{
    [MongoDB.EntityFrameworkCore.Collection("Answer")]
    public class Answer : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
