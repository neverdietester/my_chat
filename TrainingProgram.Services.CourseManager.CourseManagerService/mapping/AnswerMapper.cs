using AutoMapper;
using Trainingprogram.Contracts.CourseManager.Answer;
using TrainingProgram.Entities.CourseEntities;

namespace TrainingProgram.Services.CourseManagerService.mapping
{
    public class AnswerMapper : Profile
    {
        public AnswerMapper()
        {
            CreateMap<AnswerDTO, Answer>().ReverseMap();
            CreateMap<AnswerUpdateDTO, Answer>().ReverseMap();
        }
    }
}
