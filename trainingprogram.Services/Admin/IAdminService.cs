using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.User;
using TrainingProgram.Entities.UserEntity;
using TrainingProgram.Entities.Result;
using TrainingProgram.Contracts.Admin;

namespace Trainingprogram.Services.Abstractions.Admin
{
    public interface IAdminService
    {
        Task<BaseResult<AdminDto>> GetUsers();

        Task<BaseResult<UserBanDto>> BanUser(UserBanDto dto);

        Task<BaseResult<UserBanDto>> UnbanUser(UserBanDto dto);
    }
}
