using AutoMapper;
using Trainingprogram.Contracts.CourseManager.Question;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Services.CourseManagerService.mapping
{
    public class QuestionMapper : Profile
    {
        public QuestionMapper()
        {
            CreateMap<QuestionDTO, Question>().ReverseMap();
            CreateMap<QuestionUpdateDTO, Question>().ReverseMap();
        }
    }
}
