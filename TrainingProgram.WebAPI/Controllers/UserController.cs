using Microsoft.AspNetCore.Mvc;
using Trainingprogram.Contracts.User;
using Trainingprogram.Services.Abstractions.User;
using TrainingProgram.Entities.Result;

namespace TrainingProgram.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<BaseResult<RegisterDto>>> Register([FromBody] RegisterDto dto)
        {
            var response = await _userService.Register(dto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        /// <summary>
        /// лог пользователя 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginDto dto)
        {
            var response = await _userService.Login(dto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
