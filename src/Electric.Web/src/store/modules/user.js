import { login, refreshToken } from '@/api/auth'
import { getInfo } from '@/api/account'
import { getToken, setToken, removeToken, getRefreshToken, setRefreshToken, removeRefreshToken } from '@/utils/auth'
import router, { resetRouter } from '@/router'

const state = {
  token: getToken(),
  refreshToken: getRefreshToken(),
  name: '',
  avatar: '',
  introduction: '',
  roles: []
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_REFRESH_TOKEN: (state, refreshToken) => {
    state.refreshToken = refreshToken
  },
  SET_INTRODUCTION: (state, introduction) => {
    state.introduction = introduction
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  }
}

const actions = {
  // user login
  login({ commit }, userInfo) {
    const { username, password, codeKey, code } = userInfo
    return new Promise((resolve, reject) => {
      login({ username: username.trim(), password: password, codeKey: codeKey, code: code }).then(response => {
        const {token, refreshToken} = response
        commit('SET_TOKEN', token)
        setToken(token)
        commit('SET_REFRESH_TOKEN', refreshToken)
        setRefreshToken(refreshToken)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  //刷新Token
  refreshToken({ commit }){
    setToken(getRefreshToken())
    commit('SET_TOKEN', getRefreshToken())
    return new Promise((resolve, reject) => {
      refreshToken().then(response => {
        const {token, refreshToken} = response
        commit('SET_TOKEN', token)
        setToken(token)
        commit('SET_REFRESH_TOKEN', refreshToken)
        setRefreshToken(refreshToken)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // get user info
  getInfo({ commit, state }) {
    return new Promise((resolve, reject) => {
      getInfo(state.token).then(response => {
        const { roles, name, avatar, introduction } = response
        // roles must be a non-empty array
        if (!roles || roles.length <= 0) {
          reject('getInfo: roles must be a non-null array!')
        }

        commit('SET_ROLES', roles)
        commit('SET_NAME', name)
        commit('SET_AVATAR', avatar||"https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif")
        commit('SET_INTRODUCTION', introduction)
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {
      commit('SET_TOKEN', '')
      commit('SET_REFRESH_TOKEN', '')
      commit('SET_ROLES', [])
      removeToken()
      removeRefreshToken()
      resetRouter()
      resolve()
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      commit('SET_REFRESH_TOKEN', '')
      commit('SET_ROLES', [])
      removeToken()
      removeRefreshToken()
      resolve()
    })
  },

  // dynamically modify permissions
  async changeRoles({ commit, dispatch }, role) {
    const token = role + '-token'

    commit('SET_TOKEN', token)
    setToken(token)

    const { roles } = await dispatch('getInfo')

    resetRouter()

    // generate accessible routes map based on roles
    const accessRoutes = await dispatch('permission/generateRoutes', roles, { root: true })
    // dynamically add accessible routes
    router.addRoutes(accessRoutes)

    // reset visited views and cached views
    dispatch('tagsView/delAllViews', null, { root: true })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
