using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trainingprogram.Services.Abstractions.Admin;
using TrainingProgram.Contracts.Admin;
using TrainingProgram.Entities.Result;

namespace TrainingProgram.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<BaseResult>> GetUser()
        {
            var response = await _adminService.GetUsers();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        /// <summary>
        /// Забанить пользователя на уровне системы
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("BanUser")]
        public async Task<ActionResult<BaseResult<UserBanDto>>> BanUser([FromBody] UserBanDto dto)
        {
            var response = await _adminService.BanUser(dto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        /// <summary>
        /// Разбанить пользователя на уровне системы 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("UnbanUser")]
        public async Task<ActionResult<BaseResult<UserBanDto>>> UnbanUser([FromBody] UserBanDto dto)
        {
            var response = await _adminService.UnbanUser(dto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
