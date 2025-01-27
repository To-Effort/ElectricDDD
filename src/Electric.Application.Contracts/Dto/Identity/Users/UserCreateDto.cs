﻿using Electric.Core.Desensitization;
using Electric.Domain.Shared.Entitys.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Electric.Application.Contracts.Dto.Identity.Users
{
    public class UserCreateDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [JsonConverter(typeof(DesensitizationConvter), 2, 4)]
        public string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }


        public string? Email { get; set; }

        /// <summary>
        /// 全名：姓名
        /// </summary>
        [MaxLength(20)]
        public string? FullName { get; set; }

        /// <summary>
        /// 状态，0：禁用，1：正常
        /// </summary>
        [Required]
        public UserStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string? Remark { get; set; }
    }
}
