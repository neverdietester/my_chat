using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Requests
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }

}
