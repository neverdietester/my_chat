using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainingprogram.Contracts.Shared.Messages
{
    public class GetCourseFromAuthorRequest
    {
        public Guid AuthorId { get; set; }
    }
}
