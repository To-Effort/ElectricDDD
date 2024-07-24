namespace Electric.Domain.Shared.Entitys.Identity
{
    /// <summary>
    /// 审核日志类型
    /// </summary>
    public enum AuditLogType
    {
        /// <summary>
        /// 正常日志记录
        /// </summary>
        Info,

        /// <summary>
        /// 异常日志
        /// </summary>
        Exception = 99
    }
}
