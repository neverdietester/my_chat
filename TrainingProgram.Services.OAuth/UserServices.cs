using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Trainingprogram.Contracts.User;
using Trainingprogram.RepositoriesAbstractions.UserRepository;
using Trainingprogram.Services.Abstractions.Token;
using Trainingprogram.Services.Abstractions.User;
using TrainingProgram.Entities.Enum;
using TrainingProgram.Entities.Resources;
using TrainingProgram.Entities.Result;
using TrainingProgram.Entities.UserEntity;

namespace TrainingProgram.Services.OAuth
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;
        private readonly IRolesUserRepository _UserRoleRepository;
        public UserServices(IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository, ITokenService tokenService, ITokenRepository tokenRepository, IRolesUserRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
            _UserRoleRepository = userRoleRepository;
        }
        public async Task<BaseResult<TokenDto>> Login(LoginDto loginDto)
        {
            var user = _userRepository.GetAll()
                 .Include(x => x.Roles)
                 .FirstOrDefault(x => x.Login == loginDto.Login);
            if (user == null)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
            if (user.BanUser == true)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.UserIsBanned,
                    ErrorCode = (int)ErrorCodes.UserIsBanned
                };
            }
            if (!IsVerifyPassword(user.Password, loginDto.Password))
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.PasswordIsWrong,
                    ErrorCode = (int)ErrorCodes.PasswordIsWrong
                };
            }
            var userToken = _tokenRepository.GetAll().FirstOrDefault(x => x.UserId == user.Id);
            var userRoles = user.Roles;
            var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.role)).ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.Login));
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var RefreshToken = _tokenService.GenerateRefreshToken();

            if (userToken == null)
            {
                userToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = RefreshToken,
                    RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7)
                };
                _tokenRepository.Add(userToken);
                _tokenRepository.SaveChanges();
            }
            else
            {
                userToken.RefreshToken = RefreshToken;
                userToken.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);

                _tokenRepository.Update(userToken);
                _tokenRepository.SaveChanges();
            }
            return new BaseResult<TokenDto>()
            {
                Data = new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = RefreshToken
                }
            };
        }

        public async Task<BaseResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                    ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
                };
            }

            var user = _userRepository.GetAll().FirstOrDefault(x => x.Login == registerDto.Login);
            if (user != null)
            {
                throw new Exception($"пользователь с {user.Login} уже существует");
            }
            var hashUserPassword = HashPassword(registerDto.Password);
            try
            {
                if (user == null)
                {
                    user = new User()
                    {
                        Login = registerDto.Login,
                        Password = hashUserPassword,
                        FirstName = registerDto.FirstName,
                        LastName = registerDto.LastName,
                        Email = registerDto.Email,
                        DescriptionBlock = " "
                    };
                }
                _userRepository.Add(user);
                _userRepository.SaveChanges();

                var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.role == nameof(EnumRole.User));
                RoleUsers roleUsers = new RoleUsers()
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };
                _UserRoleRepository.Add(roleUsers);
                _UserRoleRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.RegistrationFailed,
                    ErrorCode = (int)ErrorCodes.RegistrationFailed,
                };
            }
            return new BaseResult<UserDto>
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        private string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
        private bool IsVerifyPassword(string userHashPassword, string userPassword)
        {
            var hash = HashPassword(userPassword);
            return userHashPassword == hash;
        }
    }
}
