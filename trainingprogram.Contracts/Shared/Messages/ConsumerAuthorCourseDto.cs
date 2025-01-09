using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Lesson;

namespace Trainingprogram.Contracts.Shared.Messages
{
    public class ConsumerAuthorCourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public List<LessonCreateDTO> Lessons { get; set; }

    }
}
