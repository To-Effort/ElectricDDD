using Electric.Core.Linq;
using Electric.Domain.Entitys.Identity;
using Electric.Domain.Shared.Entitys.Identity;
using System.Linq.Expressions;

namespace Electric.Domain.Specifications.AuditLogs
{
    /// <summary>
    /// 审核日志搜索过来规约
    /// </summary>
    public class AuditLogSearchFilterSpecification : Specification<EleAuditLog>
    {
        /// <summary>
        ///  API接口地址
        /// </summary>
        private string? _apiUrl;

        /// <summary>
        /// 客户端IP
        /// </summary>
        private string? _clientIpAddress;

        /// <summary>
        /// 创建者
        /// </summary>
        private Guid? _creatorId;

        /// <summary>
        /// 开始创建时间
        /// </summary>
        private DateTime? _startCreationTime;

        /// <summary>
        /// 结束创建时间
        /// </summary>
        private DateTime? _endCreationTime;

        /// <summary>
        /// 日志类型
        /// </summary>
        private List<AuditLogType>? _auditLogTypes;

        public AuditLogSearchFilterSpecification(string? apiUrl, string? clientIpAddress, Guid? creatorId,
            DateTime? startCreationTime, DateTime? endCreationTime, List<AuditLogType>? auditLogTypes)
        {
            _apiUrl = apiUrl;
            _clientIpAddress = clientIpAddress;
            _creatorId = creatorId;
            _startCreationTime = startCreationTime;
            _endCreationTime = endCreationTime;
            _auditLogTypes = auditLogTypes;
        }

        /// <summary>
        /// 获取表示当前规约的LINQ表达式
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Expression<Func<EleAuditLog, bool>> ToExpression()
        {
            Expression<Func<EleAuditLog, bool>> filter = PredicateBuilder.True<EleAuditLog>();
            if (!string.IsNullOrEmpty(_apiUrl))
            {
                filter = filter.Compose(x => x.ApiUrl.StartsWith(_apiUrl), Expression.AndAlso);
            }
            if (!string.IsNullOrEmpty(_clientIpAddress))
            {
                filter = filter.Compose(x => x.ClientIpAddress.StartsWith(_clientIpAddress), Expression.AndAlso);
            }
            if (_creatorId != null)
            {
                filter = filter.Compose(x => x.CreatorId.Equals(_creatorId), Expression.AndAlso);
            }
            if (_startCreationTime != null)
            {
                filter = filter.Compose(x => x.CreationTime >= _startCreationTime, Expression.AndAlso);
            }
            if (_endCreationTime != null)
            {
                filter = filter.Compose(x => x.CreationTime <= _endCreationTime, Expression.AndAlso);
            }
            if (_auditLogTypes != null && _auditLogTypes.Count > 0)
            {
                filter = filter.Compose(x => _auditLogTypes.Contains(x.AuditLogType), Expression.AndAlso);
            }
            return filter;
        }
    }
}
