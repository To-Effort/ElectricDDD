using Electric.Application.Exception;
using Electric.Domain.Entitys.Identity;
using Electric.Domain.Manager.Identity;
using Electric.Domain.Shared.Entitys.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Electric.Application.AuditLog
{
    /// <summary>
    /// 日志过滤器
    /// </summary>
    public class EletricAuditLogActionFilterAttribute : ActionFilterAttribute
    {
        private EleAuditLog _auditLog;
        private readonly AuditLogManager _auditLogManager;
        private readonly UserManager<EleUser> _userManager;
        private readonly ILogger<ElectricExceptionFilterAttribute> _logger;

        /// <summary>
        /// 执行前的时间
        /// </summary>
        private DateTime _executionBefore;

        /// <summary>
        /// 执行后的世界
        /// </summary>
        private DateTime _executionAfter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditLogManager"></param>
        /// <param name="userManager"></param>
        public EletricAuditLogActionFilterAttribute(AuditLogManager auditLogManager, UserManager<EleUser> userManager, ILogger<ElectricExceptionFilterAttribute> logger)
        {
            _auditLog = new EleAuditLog();
            _auditLogManager = auditLogManager;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _executionBefore = DateTime.Now;
            _auditLog = AuditLogHelper.GenerateRequestAuditLog(context, _userManager);
        }

        /// <summary>
        /// 方法执行完毕
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _executionAfter = DateTime.Now;
            _auditLog.ExecutionDuration = (int)(_executionAfter - _executionBefore).TotalMilliseconds;
            _auditLog.ReturnValue = context.Result == null ? string.Empty : JsonConvert.SerializeObject(context.Result);

            //异常信息
            if (context.Exception != null)
            {
                _auditLog.ExceptionMessage = context.Exception.Message;
                if (_auditLog.ExceptionMessage.Length > 1024)
                {
                    _auditLog.ExceptionMessage = _auditLog.ExceptionMessage.Substring(0, 1024);
                }

                _auditLog.Exception = context.Exception.ToString();
                if (_auditLog.Exception.Length > 2000)
                {
                    _auditLog.Exception = _auditLog.Exception.Substring(0, 2000);
                }
                _auditLog.AuditLogType = AuditLogType.Exception;

                //记录文件异常日志
                _logger.LogError(context.Exception, "接口：{0}\r\nMethod：{1}\r\n参数：{2}\r\nIP：{3}\r\n花费时长：{4}",
                    _auditLog.ApiUrl, _auditLog.Method, _auditLog.Parameters, _auditLog.ClientIpAddress, _auditLog.ExecutionDuration);
            }

            //插入日志至数据库
            try
            {
                //获取审核日志属性
                var _auditLogAttribute = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x.GetType() == typeof(AuditLogAttribute)) as AuditLogAttribute;

                //未【关闭记录日志】
                if (_auditLogAttribute == null || _auditLogAttribute.OpenLog)
                {
                    _ = _auditLogManager.CreateAsync(_auditLog).Result;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "保存日志至数据库异常");
            }
        }
    }
}