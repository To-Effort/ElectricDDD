using Electric.Domain.Entitys.Identity;
using Electric.Domain.Repository.Identity;

namespace Electric.EntityFrameworkCore.Repository.Identity
{
    /// <summary>
    /// 日志仓储
    /// </summary>
    public class EFCoreAuditLogRepository : EfCoreRepository<EleAuditLog, Guid>, IAuditLogRepository
    {
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="db"></param>
        public EFCoreAuditLogRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
