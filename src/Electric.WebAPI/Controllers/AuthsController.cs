using Electric.Application.Auth;
using Electric.Application.Contracts.AppService.Identity;
using Electric.Application.Contracts.Dto.Identity.Auths;
using Electric.WebAPI.Captcha;
using Electric.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;

namespace Electric.WebAPI.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private IAuthAppService _authService;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="authAppService"></param>
        /// <param name="memoryCache"></param>
        public AuthsController(IAuthAppService authAppService, IMemoryCache memoryCache)
        {
            _authService = authAppService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="authLoginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [EnableRateLimiting(RateLimiterPolicyName.LoginPolicy)]
        public async Task<IActionResult> Login([FromBody] AuthLoginDto authLoginDto)
        {
            //验证码校验
            if (string.IsNullOrEmpty(authLoginDto.CodeKey))
            {
                return StatusCode(StatusCodes.Status501NotImplemented, "验证码错误！");
            }
            else
            {
                var code = _memoryCache.Get(authLoginDto.CodeKey);
                if (code == null || !authLoginDto.Code.ToLower().Equals(code.ToString().ToLower()))
                {
                    return StatusCode(StatusCodes.Status501NotImplemented, "验证码错误！");
                }
            }
            
            var token = await _authService.LoginAsync(authLoginDto);

            //返回token
            return Ok(token);
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [HttpPost("refreshToken")]
        [EletricAuthorize]
        public async Task<IActionResult> RefreshToken()
        {
            var token = await _authService.RefreshToken();

            //返回token
            return Ok(token);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("code")]
        public IActionResult Code()
        {
            //生成验证码
            var code = CaptchaGenerator.CreateValidateCode(4);
            var buffer = CaptchaGenerator.GenerateCode(code, 90, 30);

            //验证码唯一识别Key
            var codeKey = Guid.NewGuid().ToString();

            //验证码放在内存
            _memoryCache.Set(codeKey, code, DateTimeOffset.Now.AddMinutes(5));

            return Ok(new
            {
                codeKey,
                image = "data:image/png;base64," + Convert.ToBase64String(buffer)
            });
        }
    }
}
