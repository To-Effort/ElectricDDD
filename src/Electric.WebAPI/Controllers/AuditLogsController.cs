using Electric.Application.AuditLog;
using Electric.Application.Auth;
using Electric.Application.Contracts.AppService.Identity;
using Electric.Application.Contracts.Dto.Identity.AudiLogs;
using Microsoft.AspNetCore.Mvc;

namespace Electric.WebAPI.Controllers
{
    /// <summary>
    /// 审核日志
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EletricAuthorize]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="auditLogService"></param>
        public AuditLogsController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        /// <summary>
        /// 根据搜索，分页返回审核日志列表
        /// </summary>
        /// <param name="audiLogPageRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [AuditLog(OpenLog = false)]
        public async Task<IActionResult> Get([FromQuery] AudiLogPageRequestDto audiLogPageRequestDto)
        {
            var auditLogPageResponseDto = await _auditLogService.GetPagedListAsync(audiLogPageRequestDto);
            return Ok(auditLogPageResponseDto);
        }

        /// <summary>
        /// 删除审核日志
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [AuditLog(OpenLog = false)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _auditLogService.DeleteAsync(id);
            return NoContent();
        }
    }
}
