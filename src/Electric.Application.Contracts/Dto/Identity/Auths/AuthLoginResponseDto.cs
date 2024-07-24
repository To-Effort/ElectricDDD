namespace Electric.Application.Contracts.Dto.Identity.Auths
{
    public class AuthLoginResponseDto
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
