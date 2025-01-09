using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.CourseEntities
{
    [MongoDB.EntityFrameworkCore.Collection("Course")]
    public class Course : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
