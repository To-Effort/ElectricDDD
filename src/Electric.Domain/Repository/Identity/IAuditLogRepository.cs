using Electric.Domain.Entitys.Identity;

namespace Electric.Domain.Repository.Identity
{
    /// <summary>
    /// 日志仓储
    /// </summary>
    public interface IAuditLogRepository : IBasicRepository<EleAuditLog, Guid>
    {
    }
}