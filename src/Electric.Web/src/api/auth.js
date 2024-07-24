import request from '@/utils/request'

export function login(data) {
  return request({
    url: '/auths/login',
    method: 'post',
    data
  })
}

//获取验证码
export function getCode() {
  return request({
    url: '/auths/code',
    method: 'get'
  })
}

//刷新Token
export function refreshToken() {
  return request({
    url: '/auths/refreshToken',
    method: 'post'
  })
}