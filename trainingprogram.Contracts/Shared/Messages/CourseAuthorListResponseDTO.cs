using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager.Course;

namespace Trainingprogram.Contracts.Shared.Messages
{
    public class CourseAuthorListResponseDTO
    {
        public List<CourseResponseDTO> Courses { get; set; }
    }
}
