import request from '@/utils/request'

//获取审核日志列表
export function queryAuditLogs(query) {
  return request({
    url: '/auditlogs',
    method: 'get',
    params: query
  })
}

//删除审核日志
export function deleteAuditLog(id) {
  return request({
    url: '/auditlogs/' + id,
    method: 'delete',
  })
}