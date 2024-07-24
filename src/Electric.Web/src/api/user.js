import request from '@/utils/request'

//获取用户列表
export function queryUsers(query) {
  return request({
    url: '/users',
    method: 'get',
    params: query
  })
}

//更新用户
export function updateUser(data) {
  return request({
    url: '/users/' + data.id,
    method: 'put',
    data
  })
}

//删除用户
export function deleteUser(id) {
  return request({
    url: '/users/' + id,
    method: 'delete',
  })
}

//新增用户
export function createUser(data) {
  return request({
    url: '/users',
    method: 'post',
    data
  })
}

//获取所有用户列表
export function queryAllUsers(query) {
  return request({
    url: '/users/all',
    method: 'get',
    params: query
  })
}