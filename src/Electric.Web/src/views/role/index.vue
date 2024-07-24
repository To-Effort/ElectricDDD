<template>
    <div class="app-container">
      <!--第1部分：搜索-->
      <div class="filter-container">
        <el-input v-model="listQuery.name" :placeholder="$t('role.roleName')" style="width: 200px;" class="filter-item"
          @keyup.enter.native="handleFilter" />
  
        <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search"
          @click="handleFilter">
          {{ $t('table.search') }}
        </el-button>
        <el-button v-permission="'system.role.add'" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
          {{ $t('table.add') }}
        </el-button>
      </div>

      <!--第2部分：表格-->
      <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%;">
        <el-table-column :label="$t('table.id')" type="index" align="center" width="80">
        </el-table-column>
        <el-table-column :label="$t('table.date')" width="150px" align="center">
          <template slot-scope="{row}">
            <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
          </template>
        </el-table-column>
        <el-table-column :label="$t('role.roleName')" width="150px" align="center">
          <template slot-scope="{row}">
            <span>{{ row.name }}</span>
          </template>
        </el-table-column>
        <el-table-column :label="$t('table.status')" class-name="status-col" width="100">
          <template slot-scope="{row}">
            <el-tag :type="row.status | statusFilter">
              {{ row.status | statusTextFilter }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column :label="$t('table.author')" width="110px" align="center">
          <template slot-scope="{row}">
            <span>{{ row.creator ? row.creator.userName : '-' }}</span>
          </template>
        </el-table-column>
        <el-table-column :label="$t('table.remark')" align="center">
          <template slot-scope="{row}">
            <span>{{ row.remark }}</span>
          </template>
        </el-table-column>
  
  
        <el-table-column :label="$t('table.actions')" align="center" width="200" class-name="small-padding fixed-width">
          <template slot-scope="{row,$index}">
            <el-button type="primary" v-permission="'system.role.edit'" size="mini" @click="handleUpdate(row)">
              {{ $t('table.edit') }}
            </el-button>
            <el-button v-permission="'system.role.delete'" size="mini" type="danger" @click="handleDelete(row, $index)">
              {{ $t('table.delete') }}
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <!--第3部分：翻页-->
      <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.prePage"
        @pagination="getList" />
  
      <!--第4部分：添加/编辑对话框-->
      <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
        <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="70px"
          style="width: 400px; margin-left:50px;">
          <el-form-item :label="$t('role.roleName')" prop="name">
            <el-input v-model="temp.name" type="text" placeholder="请输入" />
          </el-form-item>
          <el-form-item :label="$t('table.status')" prop="status">
            <el-radio-group v-model="temp.status" size="large">
              <el-radio-button label="正常" />
              <el-radio-button label="禁用" />
            </el-radio-group>
          </el-form-item>
          <el-form-item :label="$t('table.remark')" prop="remark">
            <el-input v-model="temp.remark" :autosize="{ minRows: 2, maxRows: 4 }" type="textarea" placeholder="请输入" />
          </el-form-item>
        </el-form>
  
        <div slot="footer" class="dialog-footer">
          <el-button @click="dialogFormVisible = false">
            {{ $t('table.cancel') }}
          </el-button>
          <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
            {{ $t('table.confirm') }}
          </el-button>
        </div>
      </el-dialog>
    </div>
</template>

<script>
import { queryRoles, updateRole, deleteRole, createRole } from '@/api/role'
import waves from '@/directive/waves' // waves directive
import { parseTime } from '@/utils'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import permission from '@/directive/permission/index'

export default {
    name: 'Role',
    components: { Pagination },
    directives: { waves, permission },
    filters: {
      statusFilter(status) {
        const statusMap = {
          '0': 'danger',
          '1': 'success',
          deleted: 'danger'
        }
        return statusMap[status]
      },
      statusTextFilter(status) {
        const statusMap = {
          '0': '禁用',
          '1': '正常'
        }
        return statusMap[status]
      }
    },
    data() {
      return {
        tableKey: 0,
        list: null,
        total: 0,
        listLoading: true,
        listQuery: {
          page: 1,
          prePage: 20,
          importance: undefined,
          title: undefined,
          type: undefined
        },
        temp: {
          id: undefined,
          roleName: '',
          fullName: '',
          remark: '',
          status: '正常'
        },
        dialogFormVisible: false,
        dialogStatus: '',
        textMap: {
          update: '编辑',
          create: '创建'
        },
        rules: {
          name: [{ required: true, message: '角色名必输', trigger: 'blur' }],
          status: [{ required: true, message: '状态必选', trigger: 'blur' }]
        }
      }
    },
    created() {
      this.getList()
    },
    methods: {
      getList() {
        this.listLoading = true
        queryRoles(this.listQuery).then(response => {
          this.list = response.roles
          this.total = response.total
  
          this.listLoading = false
        })
      },
      handleFilter() {
        this.listQuery.page = 1
        this.getList()
      },
      resetTemp() {
        this.temp = {
          id: undefined,
          roleName: '',
          fullName: '',
          remark: '',
          status: '正常'
        }
      },
      handleCreate() {
        this.resetTemp()
        this.dialogStatus = 'create'
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs['dataForm'].clearValidate()
        })
      },
      createData() {
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            let data = Object.assign({}, this.temp)
            data.status = data.status == '正常' ? 1 :0
            createRole(data).then(() => {
              this.getList()
              this.dialogFormVisible = false
              this.$notify({
                title: '成功',
                message: '创建成功',
                type: 'success',
                duration: 2000
              })
            })
          }
        })
      },
      handleUpdate(row) {
        this.temp = Object.assign({}, row) // copy obj
        this.temp.status = this.temp.status == 1 ? '正常' : '禁用';
        this.dialogStatus = 'update'
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs['dataForm'].clearValidate()
        })
      },
      updateData() {
        this.$refs['dataForm'].validate((valid) => {
          if (valid) {
            const tempData = Object.assign({}, this.temp)
            tempData.status = tempData.status == '正常' ? 1 : 0
            updateRole(tempData).then(() => {
              const index = this.list.findIndex(v => v.id === this.temp.id)
              this.temp.status = this.temp.status == '正常' ? 1 : 0
              console.log(this.temp)
              this.list.splice(index, 1, this.temp)
              this.dialogFormVisible = false
              this.$notify({
                title: '成功',
                message: '更新成功',
                type: 'success',
                duration: 2000
              })
            })
          }
        })
      },
      handleDelete(row, index) {
        this.$confirm('确认删除角色?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          deleteRole(row.id).then((response) => {
            if (response && response.code != 200) {
                this.$notify({
                 type: 'info',
                 message: '删除失败，错误原因：' + response.message
                });
            } else {
                this.$notify({
                 title: '成功',
                 message: '更新成功',
                 type: 'success',
                 duration: 2000
                })
                this.list.splice(index, 1)
            }
          })
        }).catch(() => {
          this.$notify({
            type: 'info',
            message: '已取消删除'
          });
        });
      }
    }
  }
</script>