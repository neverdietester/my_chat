using Trainingprogram.RepositoriesAbstractions.UserRepository;
using Trainingprogram.Services.Abstractions.Admin;
using TrainingProgram.Contracts.Admin;
using TrainingProgram.Entities.Enum;
using TrainingProgram.Entities.Resources;
using TrainingProgram.Entities.Result;

namespace TrainingProgram.services.Administration
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResult<UserBanDto>> BanUser(UserBanDto dto)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == dto.Guid);

            if (user == null)
            {
                return new BaseResult<UserBanDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
            else
            {
                user.BanUser = true;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }
            return new BaseResult<UserBanDto>()
            {
                Data = dto,
                ErrorMessage = null,
                ErrorCode = 0
            };
        }

        public Task<BaseResult<AdminDto>> GetUsers()
        {
            var users = _userRepository.GetAll()
                               .Select(user => new
                               {
                                   user.Id,
                                   user.Login,
                                   user.Email,
                                   user.FirstName,
                                   Roles = string.Join(", ", user.Roles.Select(r => r.role)),
                                   user.BanUser,
                                   user.DescriptionBlock
                               })
                               .ToList();

            var result = new BaseResult<AdminDto>()
            {
                Data = new AdminDto()
                {
                    Guids = users.Select(u => u.Id).ToList(),
                    Login = users.Select(u => u.Login).ToList(),
                    Email = users.Select(u => u.Email).ToList(),
                    FirstName = users.Select(u => u.FirstName).ToList(),
                    Roles = users.Select(u => u.Roles).ToList(),
                    BanDescription = users.Select(u => u.DescriptionBlock).ToList(),
                    isBan = users.Select(u => u.BanUser).ToList()
                }
            };

            return Task.FromResult(result);
        }

        public async Task<BaseResult<UserBanDto>> UnbanUser(UserBanDto dto)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == dto.Guid);

            if (user == null)
            {
                return new BaseResult<UserBanDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
            else
            {
                user.BanUser = false;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }
            return new BaseResult<UserBanDto>()
            {
                Data = dto,
                ErrorMessage = null,
                ErrorCode = 0
            };
        }
    }
}
