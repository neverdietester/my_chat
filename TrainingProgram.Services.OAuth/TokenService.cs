using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Trainingprogram.Contracts.User;
using Trainingprogram.RepositoriesAbstractions.UserRepository;
using Trainingprogram.Services.Abstractions.Token;
using TrainingProgram.Entities.Resources;
using TrainingProgram.Entities.Result;
using TrainingProgram.Entities.Settings;

namespace TrainingProgram.Services.OAuth
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository userRepository;
        private readonly string _jwtKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TokenService(IOptions<JwtSettings> options, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            _jwtKey = options.Value.JwtKey;
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken =
                new JwtSecurityToken(_issuer, _audience, claims, null, DateTime.UtcNow.AddMinutes(10), credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumbers = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumbers);
            return Convert.ToBase64String(randomNumbers);
        }
        private string GetRefreshTokenFromCookie()
        {
            var refreshTokenCookie = _httpContextAccessor.HttpContext.Request.Cookies["RefreshToken"];
            return refreshTokenCookie.ToString();
        }
        public void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7) 
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, cookie);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string AccessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                ValidateLifetime = true,
                ValidAudience = _audience,
                ValidIssuer = _issuer
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = TokenHandler.ValidateToken(AccessToken, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenArgumentException(ErrorMessage.InvalidToken);
            return claimsPrincipal;
        }

        public async Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto)
        {
            var refreshToken = GetRefreshTokenFromCookie();
            var accessToken = dto.AccessToken;

            var claimsPrincipal = GetPrincipalFromExpiredToken(accessToken);
            var userName = claimsPrincipal.Identity?.Name;

            var user = userRepository.GetAll().Include(x => x.UserToken)
                .FirstOrDefault(x => x.Login == userName);
            if (user == null || user.UserToken.RefreshToken != refreshToken ||
                user.UserToken.RefreshTokenExpireTime <= DateTime.UtcNow)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidToken
                };
            }

            var newAccessToken = GenerateAccessToken(claimsPrincipal.Claims);
            var newRefreshToken = GenerateRefreshToken();

            
            SetRefreshTokenInCookie(newRefreshToken);

            return new BaseResult<TokenDto>()
            {
                Data = new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                }
            };
        }
    }
}
