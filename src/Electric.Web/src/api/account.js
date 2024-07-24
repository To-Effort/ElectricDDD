import request from '@/utils/request'

//获取登录用户信息
export function getInfo() {
  return request({
    url: '/accounts',
    method: 'get'
  })
}

//获取权限列表
export function getPermissions() {
  return request({
    url: '/accounts/permissions',
    method: 'get'
  })
}

//更新密码
export function updatePassword(data) {
  return request({
    url: '/accounts/password',
    method: 'patch',
    data
  })
}