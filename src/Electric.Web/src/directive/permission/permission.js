import store from '@/store'

function checkPermission(el, binding) {
  const { value } = binding

  if (value && value.length > 0) {
    const permissions = store.getters.permissions
    const permissionCode = value

    const hasPermission = permissions.some(x => {
      return x.code == permissionCode && x.permissionType == 1
    })

    if (!hasPermission) {
      el.parentNode && el.parentNode.removeChild(el)
    }
  } else {
    throw new Error(`need value Like v-permission="'system.user.add'"`)
  }
}

export default {
  inserted(el, binding) {
    checkPermission(el, binding)
  },
  update(el, binding) {
    checkPermission(el, binding)
  }
}