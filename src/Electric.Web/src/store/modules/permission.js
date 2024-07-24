import { asyncRoutes, constantRoutes } from '@/router'
import { getPermissions } from '@/api/account'

/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(roles, route) {
  if (route.meta && route.meta.roles) {
    return roles.some(role => route.meta.roles.includes(role))
  } else {
    return true
  }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, roles) {
  const res = []

  routes.forEach(route => {
    const tmp = { ...route }
    if (hasPermission(roles, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, roles)
      }
      res.push(tmp)
    }
  })

  return res
}

const state = {
  routes: [],
  addRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
  },
  SET_PERMISSIONS: (state, permissions) => {
    state.permissions = permissions
  }
}

const actions = {
  generateRoutes({ commit }, roles) {
    return new Promise(resolve => {
      let accessedRoutes
      getPermissions().then(response => {
        let permissions = response
        //初始化，默认隐藏所有菜单
        for(let routeIndex in asyncRoutes)
        {
            asyncRoutes[routeIndex].hidden = true
            let childrens = asyncRoutes[routeIndex].children
            for(let childrenIndex in childrens)
            {
              childrens[childrenIndex].hidden = true
            }
        }

        //开启有权限的菜单
        for(let routeIndex in asyncRoutes)
        {
            let childrens = asyncRoutes[routeIndex].children
            for(let childrenIndex in childrens)
            {
              let child = childrens[childrenIndex]
              if(permissions.find(x=>x.code == child.name) != null)
              {
                child.hidden = false;
                asyncRoutes[routeIndex].hidden = false;
              }
            }
        }
        accessedRoutes = asyncRoutes
        commit('SET_ROUTES', accessedRoutes)
        commit('SET_PERMISSIONS', permissions)
        resolve(accessedRoutes)
      })
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
