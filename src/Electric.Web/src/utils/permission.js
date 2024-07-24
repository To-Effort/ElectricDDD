import store from '@/store'

/**
 * @param {Array} value
 * @returns {Boolean}
 * @example see @/views/permission/directive.vue
 */
export default function checkPermission(value) {
  if (value && value.length > 0) {
    const permissions = store.getters.permissions
    const permissionCode = value

    const hasPermission = permissions.some(x => {
      return x.code == permissionCode && x.permissionType == 1
    })
    return hasPermission
  } else {
    console.error(`need permission! Like v-permission="system.users.add"`)
    return false
  }
}