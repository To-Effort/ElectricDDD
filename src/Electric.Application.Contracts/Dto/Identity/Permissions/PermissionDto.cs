﻿using Electric.Domain.Shared.Entitys.Identity;

namespace Electric.Application.Contracts.Dto.Identity.Permissions
{
    /// <summary>
    /// 菜单权限信息
    /// </summary>
    public class PermissionDto
    {
        public Guid Id { get; set; }

        public Guid? CreatorId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 权限编码
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Url地址
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Vue页面组件
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 菜单类型：菜单权限、元素权限、Api权限、数据权限
        /// </summary>
        public PermissionType PermissionType { get; set; }

        /// <summary>
        /// API方法
        /// </summary>
        public string? ApiMethod { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 状态，0：禁用，1：正常
        /// </summary>
        public PermissionStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}
