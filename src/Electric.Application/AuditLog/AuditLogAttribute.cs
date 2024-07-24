using Microsoft.AspNetCore.Mvc.Filters;

namespace Electric.Application.AuditLog
{
    /// <summary>
    /// 审核日志开关
    /// </summary>
    public class AuditLogAttribute : Attribute
    {
        /// <summary>
        /// 关闭记录日志
        /// </summary>
        public bool OpenLog { get; set; } = false;
    }
}
