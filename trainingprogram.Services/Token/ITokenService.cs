using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Trainingprogram.Contracts.User;
using TrainingProgram.Entities.Result;

namespace Trainingprogram.Services.Abstractions.Token
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string AccessToken);

        Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
    }
}
