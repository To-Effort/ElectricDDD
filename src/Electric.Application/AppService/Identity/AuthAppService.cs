using Electric.Application.AppService.Base;
using Electric.Application.Auth;
using Electric.Application.Contracts.AppService.Identity;
using Electric.Application.Contracts.Dto.Identity.Auths;
using Electric.Core.Exceptions;
using Electric.Domain.Manager.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Electric.Application.AppService.Identity
{
    public class AuthAppService : BaseAppService, IAuthAppService
    {
        /// <summary>
        /// JWT配置
        /// </summary>
        private readonly JwtBearerSetting _jwtBearerSetting;

        /// <summary>
        /// 用户管理器
        /// </summary>
        private readonly UserManager _userManager;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="jwtBearerSetting"></param>
        /// <param name="userManager"></param>
        public AuthAppService(JwtBearerSetting jwtBearerSetting, UserManager userManager)
        {
            _jwtBearerSetting = jwtBearerSetting;
            _userManager = userManager;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="authLoginDto"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<AuthLoginResponseDto> LoginAsync(AuthLoginDto authLoginDto)
        {
            //根据用户名、密码校验
            var user = await _userManager.FindByNameAsync(authLoginDto.UserName);
            if (user == null)
            {
                throw new BusinessException("登录失败，账号或密码错误");
            }

            //登录验证
            var succeeded = await _userManager.CheckPasswordAsync(user, authLoginDto.Password);
            if (succeeded)
            {
                var authLoginResponseDto = GenerateToken(authLoginDto.UserName, user.Id.ToString());

                //返回token
                return authLoginResponseDto;
            }
            else
            {
                throw new BusinessException("登录失败，账号或密码错误");
            }
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<AuthLoginResponseDto> RefreshToken()
        {
            //返回token
            return Task.Run(() => { return GenerateToken(_eleSession.UserName, _eleSession.UserId.ToString()); });
        }


        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private AuthLoginResponseDto GenerateToken(string userName, string userId)
        {
            var authLoginResponseDto = new AuthLoginResponseDto();
            //定义JWT的Payload部分
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Sid, userId),
                };

            //生成token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerSetting.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = new JwtSecurityToken(
                issuer: _jwtBearerSetting.Issuer,
                audience: _jwtBearerSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            authLoginResponseDto.Token = jwtSecurityTokenHandler.WriteToken(securityToken);

            //生成RefreshToken
            var securityRefreshToken = new JwtSecurityToken(
                issuer: _jwtBearerSetting.Issuer,
                audience: _jwtBearerSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);
            authLoginResponseDto.RefreshToken = jwtSecurityTokenHandler.WriteToken(securityRefreshToken);

            return authLoginResponseDto;
        }
    }
}
