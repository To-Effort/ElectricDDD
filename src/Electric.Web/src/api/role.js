import request from '@/utils/request'

//获取角色所有列表
export function getAllRoles() {
  return request({
    url: '/roles/all',
    method: 'get'
  })
}

//搜索角色
export function queryRoles(query) {
  return request({
    url: '/roles',
    method: 'get',
    params: query
  })
}

//更新角色
export function updateRole(data) {
  return request({
    url: '/roles/' + data.id,
    method: 'put',
    data
  })
}

//删除角色
export function deleteRole(id) {
  return request({
    url: '/roles/' + id,
    method: 'delete'
  })
}

//新增角色
export function createRole(data) {
  return request({
    url: '/roles',
    method: 'post',
    data
  })
}

//获取角色包含的权限列表
export function getPermissions(id) {
  return request({
    url: '/roles/' + id + '/permissions',
    method: 'get'
  })
}

//保存角色包含的权限列表
export function savePermissions(id, permissionIds) {
  return request({
    url: '/roles/' + id + '/permissions',
    method: 'put',
    data:{permissionIds}
  })
}