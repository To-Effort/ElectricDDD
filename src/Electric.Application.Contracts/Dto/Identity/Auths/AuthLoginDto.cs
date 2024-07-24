using Electric.Core.Desensitization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Electric.Application.Contracts.Dto.Identity.Auths
{
    public class AuthLoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [JsonConverter(typeof(DesensitizationConvter), 2, 4)]
        public string Password { get; set; }

        /// <summary>
        /// 验证码Key
        /// </summary>
        [Required]
        public string CodeKey { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
