using Electric.Core.Exceptions;
using Electric.Core;
using Electric.Domain.Entitys.Identity;
using Electric.Domain.Repository.Identity;
using Electric.Domain.Specifications.Roles;
using Electric.Domain.Shared.Entitys.Identity;
using Electric.Domain.Specifications.AuditLogs;

namespace Electric.Domain.Manager.Identity
{
    /// <summary>
    /// 日志领域服务
    /// </summary>
    public class AuditLogManager : IDomainService
    {
        private IAuditLogRepository _auditLogRepository;
        public AuditLogManager(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="eleAuditLog"></param>
        /// <returns></returns>
        public async Task<EleAuditLog> CreateAsync(EleAuditLog eleAuditLog)
        {
            Check.NotNull(eleAuditLog, nameof(eleAuditLog));

            eleAuditLog.ApiUrl = eleAuditLog.ApiUrl != null && eleAuditLog.ApiUrl.Length > 256 ? eleAuditLog.ApiUrl.Substring(0, 256) : eleAuditLog.ApiUrl;
            eleAuditLog.Method = eleAuditLog.Method != null && eleAuditLog.Method.Length > 256 ? eleAuditLog.Method.Substring(0, 256) : eleAuditLog.Method;
            eleAuditLog.ClientIpAddress = eleAuditLog.ClientIpAddress != null && eleAuditLog.ClientIpAddress.Length > 64 ? eleAuditLog.ClientIpAddress.Substring(0, 64) : eleAuditLog.ClientIpAddress;
            eleAuditLog.BrowserInfo = eleAuditLog.BrowserInfo != null && eleAuditLog.BrowserInfo.Length > 512 ? eleAuditLog.BrowserInfo.Substring(0, 512) : eleAuditLog.BrowserInfo;
            eleAuditLog.ExceptionMessage = eleAuditLog.ExceptionMessage != null && eleAuditLog.ExceptionMessage.Length > 1024 ? eleAuditLog.ExceptionMessage.Substring(0, 1024) : eleAuditLog.ExceptionMessage;
            eleAuditLog.Exception = eleAuditLog.Exception != null && eleAuditLog.Exception.Length > 2000 ? eleAuditLog.Exception.Substring(0, 2000) : eleAuditLog.Exception;

            return await _auditLogRepository.InsertAsync(eleAuditLog, autoSave: true);
        }

        /// <summary>
        /// 根据搜索条件，获取审核日志列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="prePage"></param>
        /// <param name="apiUrl"></param>
        /// <param name="clientIpAddress"></param>
        /// <param name="creatorId"></param>
        /// <param name="startCreationTime"></param>
        /// <param name="endCreationTime"></param>
        /// <param name="auditLogTypes"></param>
        /// <returns></returns>
        public async Task<List<EleAuditLog>> GetListAsync(int page, int prePage, string? apiUrl, string? clientIpAddress, Guid? creatorId,
            DateTime? startCreationTime, DateTime? endCreationTime, List<AuditLogType>? auditLogTypes)
        {
            var specification = new AuditLogSearchFilterSpecification(apiUrl, clientIpAddress, creatorId, startCreationTime, endCreationTime, auditLogTypes);

            //返回审核日志列表
            var skipCount = (page <= 0 ? 0 : page - 1) * prePage;
            var auditLogs = await _auditLogRepository.GetListAsync(specification, skipCount, prePage, sorting: nameof(EleAuditLog.CreationTime) + " desc");

            return auditLogs;
        }

        /// <summary>
        /// 根据搜索条件，返回总记录数
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="clientIpAddress"></param>
        /// <param name="creatorId"></param>
        /// <param name="startCreationTime"></param>
        /// <param name="endCreationTime"></param>
        /// <param name="auditLogTypes"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string? apiUrl, string? clientIpAddress, Guid? creatorId,
            DateTime? startCreationTime, DateTime? endCreationTime, List<AuditLogType>? auditLogTypes)
        {
            var specification = new AuditLogSearchFilterSpecification(apiUrl, clientIpAddress, creatorId, startCreationTime, endCreationTime, auditLogTypes);

            //返回总记录数
            var total = await _auditLogRepository.GetCountAsync(specification);

            return total;
        }
    }
}
