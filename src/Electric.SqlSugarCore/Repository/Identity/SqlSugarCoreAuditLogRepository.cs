using Electric.Core.UOW;
using Electric.Domain.Entitys.Identity;
using Electric.Domain.Repository.Identity;
using Electric.SqlSugarCore.Mapping;
using Electric.SqlSugarCore.Po;
using SqlSugar;

namespace Electric.SqlSugarCore.Repository.Identity
{
    /// <summary>
    /// 日志仓储
    /// </summary>
    public class SqlSugarCoreAuditLogRepository : SqlSugarCoreRepository<EleAuditLogPo, EleAuditLog, Guid>, IAuditLogRepository
    {
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sqlSugarMapper"></param>
        /// <param name="unitOfWork"></param>
        public SqlSugarCoreAuditLogRepository(ISqlSugarClient db, SqlSugarMapper sqlSugarMapper, IUnitOfWork unitOfWork) : base(db, sqlSugarMapper, unitOfWork)
        {
        }
    }
}
