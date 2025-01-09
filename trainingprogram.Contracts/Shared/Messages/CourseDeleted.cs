using MongoDB.Bson;

namespace Trainingprogram.Contracts.Shared.Messages
{
    public class CourseDeleted
    {
        public ObjectId CourseId { get; set; }
    }
}
