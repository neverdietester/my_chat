using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.CourseManager;
using Trainingprogram.Contracts.CourseManager.Course;
using TrainingProgram.Entities.CourseEntities;
using TrainingProgram.Entities.Result;

namespace Trainingprogram.Services.Abstractions.CourseManager
{
    public interface ICourseService
    {
        public Task<CourseResponseDTO> AsyncCreateCourse(CourseCreateDTO courseDto);

        public Task<BaseResult<CourseResponseDTO>> AsyncDeleteCourse(string Id, CancellationToken cancellationToken);

        public Task<BaseResult<CourseResponseDTO>> AsyncGetCourse(string id, CancellationToken cancellationToken);

        public Task<BaseResult<CourseResponseDTO>> UpdateCourse(CourseUpdateDTO courseDto);

        public Task<BaseResult<List<CourseResponseDTO>>> AsyncGetCourseListFromAuthor(string authorId, CancellationToken cancellationToken);
    }
}
