using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Messages
{
    public class CourseCreated
    {
        public ObjectId CourseId { get; set; }  // Используем ObjectId
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
