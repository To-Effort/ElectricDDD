import axios from 'axios'
import { MessageBox, Message } from 'element-ui'
import store from '@/store'
import router from '@/router'
import { getToken, getRefreshToken } from '@/utils/auth'
import qs from 'qs'
import { setTimeout } from 'core-js'

// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 5000 // request timeout
})

// request interceptor
service.interceptors.request.use(
  config => {
    // do something before request is sent

    if (store.getters.token) {
      // let each request carry token
      // ['X-Token'] is a custom headers key
      // please modify it according to the actual situation
      config.headers['Authorization'] = 'Bearer ' + getToken()
    }
    if(config.method === 'get'){
      //如果是get请求，且params是数组类型如arr[]=1&arr[]=2，则转换成arr=1&arr=2
      config.paramsSerializer = function(params) {
          return qs.stringify(params, {arrayFormat: 'repeat'})
      }
    }
    return config
  },
  error => {
    // do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    const res = response.data

    return res
  },
  async error => {
    console.log('err' + error) // for debug
    if(error.response && error.response.status == 400)
    {
      if(error.response.data.code)
      {
        Message({
          message: error.response.data.message,
          type: 'error',
          duration: 5 * 1000
        })
      }
      else{
        Message({
          message: error.response.data.title,
          type: 'error',
          duration: 5 * 1000
        })
      }
    }
    else if(error.response && error.response.status == 401)
    {
      let auth = error.config.headers['Authorization']
      let refreshAuth = 'Bearer ' + getRefreshToken()
      if(auth != refreshAuth)
      {
        let result
        let lastRefresh = async ()=>{
          //刷新Token
          await store.dispatch('user/refreshToken').then(()=>{
            error.config.headers['Authorization'] = 'Bearer ' + getToken()
          })
          
          //重新发起上一次请求
          await service(error.config).then(response => {
            result = response
          })
        }
        await lastRefresh()
        return Promise.resolve(result)
      }
      else{
        Message({
          message: '请重新登录，认证已过期！',
          type: 'error',
          duration: 2 * 1000
        })
        setTimeout(()=>{
          store.dispatch('user/resetToken')
          router.push({path:"/login"})
        }, 2000)     
      }
    }
    else if(error.response && error.response.status == 403)
    {
      Message({
        message: '操作失败，无此权限！',
        type: 'error',
        duration: 5 * 1000
      })
    }
    else if(error.response && (error.response.status == 500||error.response.status == 501))
    {
      Message({
        message: error.response.data || '服务器异常！',
        type: 'error',
        duration: 5 * 1000
      })
    }
    else
    {
      Message({
        message: error.message,
        type: 'error',
        duration: 5 * 1000
      })
    }
    return Promise.reject(error)
  }
)

export default service
