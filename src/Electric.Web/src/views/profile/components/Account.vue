<template>
  <el-form ref="dataForm" :rules="rules" :model="user">
    <el-form-item :label="$t('profile.userName')">
      <el-input v-model.trim="user.name" disabled />
    </el-form-item>
    <el-form-item :label="$t('profile.oldPassword')" prop="oldPassword">
      <el-input v-model="user.oldPassword" type="password" />
    </el-form-item>
    <el-form-item :label="$t('profile.newPassword')" prop="newPassword">
      <el-input v-model="user.newPassword" type="password" />
    </el-form-item>
    <el-form-item :label="$t('profile.surePassword')" prop="surePassword">
      <el-input v-model="user.surePassword" type="password" />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submit">{{ $t('profile.update')}}</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import { updatePassword } from '@/api/account'

export default {
  props: {
    user: {
      type: Object,
      default: () => {
        return {
          name: '',
          email: '',
          oldPassword: '',
          newPassword: ''
        }
      }
    },
  },
  data() {
    return {
      rules: {
        oldPassword: [{ required: true, message: '旧密码必输', trigger: 'blur' }],
        newPassword: [{ required: true, message: '新密码必输', trigger: 'blur' }, {
          trigger: 'blur', validator: (rules, value, callback) => {
            if (!value) {
              callback(new Error('密码必输'))
            }
            else {
              let reg = /^.*(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?])\w{6,}/
              if (!reg.test(value)) {
                callback(new Error('密码必须大于6位，并且包含特殊字符、大小写字母'))
              }
              else {
                callback()
              }
            }
          }
        }],
        surePassword: [{ required: true, message: '新密码必输', trigger: 'blur' },{
          trigger: 'blur',
          validator: (rules, value, callback) => {
            this.user.newPassword == value ? callback() : callback(new Error('密码不一致'))
          }
        }]
      }
    }
  },
  methods: {
    submit() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          let data = Object.assign({}, this.user)
          updatePassword(data).then(() => {
            this.$notify({
              title: '成功',
              message: '更新成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    }
  }
}
</script>
