using Electric.Application.AppService.Base;
using Electric.Application.Contracts.AppService.Identity;
using Electric.Application.Contracts.Dto.Identity.AudiLogs;
using Electric.Domain.Manager.Identity;
using Electric.Domain.Repository.Identity;

namespace Electric.Application.AppService.Identity
{
    public class AuditLogService : BaseAppService, IAuditLogService
    {
        /// <summary>
        /// 审核日志领域服务
        /// </summary>
        private AuditLogManager _auditLogManager;

        /// <summary>
        /// 审核日志仓储
        /// </summary>
        private IAuditLogRepository _auditLogRepository;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="auditLogManager"></param>
        /// <param name="auditLogRepository"></param>
        public AuditLogService(AuditLogManager auditLogManager, IAuditLogRepository auditLogRepository)
        {
            _auditLogManager = auditLogManager;
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// 获取审核日志列表
        /// </summary>
        /// <param name="audiLogPageRequestDto"></param>
        /// <returns></returns>
        public async Task<AuditLogPageResponseDto> GetPagedListAsync(AudiLogPageRequestDto audiLogPageRequestDto)
        {
            var auditLogs = await _auditLogManager.GetListAsync(audiLogPageRequestDto.Page, audiLogPageRequestDto.PrePage, audiLogPageRequestDto.ApiUrl,
                audiLogPageRequestDto.ClientIpAddress, audiLogPageRequestDto.CreatorId, audiLogPageRequestDto.StartTime, audiLogPageRequestDto.EndTime,
                audiLogPageRequestDto.AuditLogTypes);

            //根据搜索条件，获取总记录数
            var total = await _auditLogManager.GetCountAsync(audiLogPageRequestDto.ApiUrl,
                audiLogPageRequestDto.ClientIpAddress, audiLogPageRequestDto.CreatorId, audiLogPageRequestDto.StartTime, audiLogPageRequestDto.EndTime,
                audiLogPageRequestDto.AuditLogTypes);

            //映射为Dto
            var auditLogDtos = _mapper.Map<List<AuditLogDto>>(auditLogs);

            //返回
            return new AuditLogPageResponseDto()
            {
                Page = audiLogPageRequestDto.Page,
                PrePage = audiLogPageRequestDto.PrePage,
                AuditLogs = auditLogDtos,
                Total = total
            };
        }

        /// <summary>
        /// 删除审核日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteAsync(Guid id)
        {
            //判断审核日志Id，是否存在
            var user = await _auditLogRepository.FindAsync(id);
            if (user == null)
            {
                return;
            }

            //删除审核日志
            await _auditLogRepository.DeleteAsync(id);

            //提交所有更新
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
