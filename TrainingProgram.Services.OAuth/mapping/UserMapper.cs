using AutoMapper;
using Trainingprogram.Contracts.User;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Services.OAuth.mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
