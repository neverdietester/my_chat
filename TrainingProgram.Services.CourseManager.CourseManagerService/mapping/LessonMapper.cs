using AutoMapper;
using Trainingprogram.Contracts.CourseManager.Lesson;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Services.CourseManagerService.mapping
{
    public class LessonMapper : Profile
    {
        public LessonMapper()
        {
            CreateMap<LessonCreateDTO, Lesson>().ReverseMap();

        }
    }
}
