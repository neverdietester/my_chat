using AutoMapper;
using Trainingprogram.Contracts.CourseManager.Course;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Services.CourseManagerService.mapping
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseCreateDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();
        }
    }
}
