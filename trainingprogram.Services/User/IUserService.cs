using Trainingprogram.Contracts.User;
using TrainingProgram.Entities.Result;


namespace Trainingprogram.Services.Abstractions.User
{
    public interface IUserService
    {
        Task<BaseResult<TokenDto>> Login(LoginDto loginDto);

        Task<BaseResult<UserDto>> Register(RegisterDto registerDto);
    }
}
