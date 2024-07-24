using Electric.Domain.Shared.Entitys.Identity;
using SqlSugar;

namespace Electric.SqlSugarCore.Po
{
    [SugarTable("EleAuditLog")]
    [SugarIndex("Index_CreationTime", nameof(CreationTime), OrderByType.Desc)]
    public class EleAuditLogPo : EntityPo<Guid>
    {
        /// <summary>
        /// API接口地址
        /// </summary>
        [SugarColumn(Length =256, ColumnDescription = "API接口地址")]
        public string ApiUrl { get; set; } = string.Empty;

        /// <summary>
        /// 接口的方法
        /// </summary>
        [SugarColumn(Length = 256, ColumnDescription = "接口的方法")]
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// 参数
        /// </summary>
        [SugarColumn(ColumnDataType ="text", ColumnDescription = "参数")]
        public string Parameters { get; set; } = string.Empty;

        /// <summary>
        /// 返回结果
        /// </summary>
        [SugarColumn(ColumnDataType = "text", ColumnDescription = "返回结果")]
        public string ReturnValue { get; set; } = string.Empty;

        /// <summary>
        /// 执行时间
        /// </summary>
        [SugarColumn(ColumnDescription = "执行时间")]
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [SugarColumn(Length = 64, ColumnDescription = "客户端IP")]
        public string ClientIpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [SugarColumn(Length = 512, ColumnDescription = "浏览器信息")]
        public string BrowserInfo { get; set; } = string.Empty;

        /// <summary>
        /// 日志类型
        /// </summary>
        [SugarColumn(ColumnDescription = "日志类型 0:正常日志记录，99：异常日志")]
        public AuditLogType AuditLogType { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [SugarColumn(Length = 1024, ColumnDescription = "异常信息")]
        public string ExceptionMessage { get; set; } = string.Empty;

        /// <summary>
        /// 详细异常
        /// </summary>
        [SugarColumn(Length = 2000, ColumnDescription = "详细异常")]
        public string Exception { get; set; } = string.Empty;
    }
}
