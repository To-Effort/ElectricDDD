using Electric.Domain.Entitys.Commons;
using Electric.Domain.Shared.Entitys.Identity;

namespace Electric.Domain.Entitys.Identity
{
    public class EleAuditLog: AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// API接口地址
        /// </summary>
        public string ApiUrl { get; set; } = string.Empty;

        /// <summary>
        /// 接口的方法
        /// </summary>
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; } = string.Empty;

        /// <summary>
        /// 返回结果
        /// </summary>
        public string ReturnValue { get; set; } = string.Empty;

        /// <summary>
        /// 执行时间
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; } = string.Empty;

        /// <summary>
        /// 日志类型
        /// </summary>
        public AuditLogType AuditLogType { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string ExceptionMessage { get; set; } = string.Empty;

        /// <summary>
        /// 详细异常
        /// </summary>
        public string Exception { get; set; } = string.Empty;

        public EleAuditLog()
        {
            Id = Guid.NewGuid();
        }
    }
}
