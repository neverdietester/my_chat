using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Events
{
    public class CourseCreatedEvent
    {
        public string Name { get; set; }
        public Guid AuthorId { get; set; }

    }
}
