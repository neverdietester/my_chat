using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainingprogram.Contracts.Shared.Messages
{
    public class ConsumerCourseDto
    {
        public string Id { get; set; }  
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public string Description { get; set; }
    }
}
