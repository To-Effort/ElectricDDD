using Electric.Application.Contracts.Dto.Identity.Auths;

namespace Electric.Application.Contracts.AppService.Identity
{
    public interface IAuthAppService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="authLoginDto"></param>
        /// <returns></returns>
        public Task<AuthLoginResponseDto> LoginAsync(AuthLoginDto authLoginDto);

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <returns></returns>
        public Task<AuthLoginResponseDto> RefreshToken();
    }
}
