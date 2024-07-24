using Electric.Application.Contracts.Dto.Identity.Commons;

namespace Electric.Application.Contracts.Dto.Identity.AudiLogs
{
    /// <summary>
    /// 审核日志翻页响应对象
    /// </summary>
    public class AuditLogPageResponseDto : PageResponseDto
    {
        /// <summary>
        /// 审核日志列表
        /// </summary>
        public List<AuditLogDto>? AuditLogs { get; set; }
    }
}
