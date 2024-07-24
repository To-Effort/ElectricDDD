using Electric.Application.Contracts.Dto.Identity.AudiLogs;
using Electric.Application.Contracts.Dto.Identity.Users;

namespace Electric.Application.Contracts.AppService.Identity
{
    public interface IAuditLogService
    {
        /// <summary>
        /// 根据搜索，分页返回审核日志列表
        /// </summary>
        /// <param name="audiLogPageRequestDto"></param>
        /// <returns></returns>
        public Task<AuditLogPageResponseDto> GetPagedListAsync(AudiLogPageRequestDto audiLogPageRequestDto);

        /// <summary>
        /// 删除审核日志
        /// </summary>
        /// <param name="id"></param>
        public Task DeleteAsync(Guid id);
    }
}
