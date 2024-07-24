<template>
    <div class="app-container" style="height:90%;">
        <el-container style="border: 1px solid #eee;">
            <el-aside width="250px" style="background-color: rgb(238, 241, 246);margin-bottom: 0;padding-bottom: 20px;">
                <div style="padding-bottom:10px ;1">
                    <el-row>
                        <el-col :span="10" :offset="2"><strong>菜单</strong></el-col>
                        <el-col :span="12"><el-button v-permission="'system.permission.add'" type="primary" icon="el-icon-edit"
                                @click="handleCreate('menu')">添加菜单</el-button></el-col>
                    </el-row>
                </div>
                <el-menu default-active="2" class="el-menu-vertical-demo" :default-openeds="menuOpeneds">
                    <template v-for="(menu, index) in treeMenus">
                        <el-submenu :index="index + ''">
                            <template slot="title">
                                <i v-if="menu.icon && menu.icon.indexOf('el-icon') >= 0" :class="menu.icon"
                                    style="margin: 0;width:18px;" />
                                <svg-icon v-if="menu.icon && menu.icon.indexOf('el-icon') < 0" :icon-class='menu.icon'
                                    style="margin: 0;width:18px;" />
                                <span>{{ menu.name }}</span>
                            </template>
                            <template v-for="child in menu.childs">
                                <el-menu-item :index="child.code">
                                    <i v-if="child.icon && child.icon.indexOf('el-icon') >= 0" :class="child.icon"
                                        style="margin: 0;width:18px;" />
                                    <svg-icon v-if="child.icon && child.icon.indexOf('el-icon') < 0" :icon-class='child.icon'
                                        style="margin: 0;width:18px;" />
                                    {{ child.name }}</el-menu-item>
                            </template>
                        </el-submenu>
                    </template>
                </el-menu>
            </el-aside>
        <el-container>
        <el-table :key="tableKey" v-loading="listLoading" :data="tableMenus" border fit highlight-current-row
            style="width: 100%;">
            <el-table-column type="expand">
                <template slot-scope="props" class-name="expand-menu">
                    <div v-if="props.row.parentId == null">
                        此菜单为目录
                    </div>
                    <template v-if="props.row.parentId != null">
                        <h3>按钮/功能权限：设置页面功能、按钮权限 <el-button @click="handleCreate('button', props.row.id)" type="primary"
                                icon="el-icon-edit" style="margin-left:10px;">添加功能权限</el-button></h3>
                        <el-table v-loading="listLoading" stripe 
                            :data="list.filter(x => x.parentId == props.row.id && x.permissionType == 1)" border fit
                            highlight-current-row style="width: 100%;">
                            <el-table-column :label="$t('table.id')" type="index" align="center" width="80">
                            </el-table-column>
                            <el-table-column :label="$t('table.date')" width="150px" align="center">
                                <template slot-scope="{row}">
                                    <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
                                </template>
                            </el-table-column>
                            <el-table-column :label="$t('permission.elementName')" width="150px" align="center">
                                <template slot-scope="{row}">
                                    <span>{{ row.name }}</span>
                                </template>
                            </el-table-column>
                            <el-table-column :label="$t('permission.permissionCode')" class-name="status-col" width="150">
                                <template slot-scope="{row}">
                                    {{ row.code }}
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
                            <el-table-column :label="$t('table.actions')" align="center" width="200"
                                class-name="small-padding fixed-width">
                                <template slot-scope="{row,$index}">
                                    <el-button v-permission="'system.permission.edit'" type="primary" size="mini" @click="handleUpdate(row)">
                                        {{ $t('table.edit') }}
                                    </el-button>
                                    <el-button v-permission="'system.permission.delete'" size="mini" type="danger"
                                        @click="handleDelete(row, $index)">
                                        {{ $t('table.delete') }}
                                    </el-button>
                                </template>
                            </el-table-column>
                        </el-table>
                        <h3>API权限：设置API访问权限 <el-button @click="handleCreate('api', props.row.id)" type="primary"
                                icon="el-icon-edit" style="margin-left:10px;">添加API权限</el-button></h3>
                        <el-table v-loading="listLoading" stripe 
                            :data="list.filter(x => x.parentId == props.row.id && x.permissionType == 2)" border fit
                            highlight-current-row style="width: 100%;">
                            <el-table-column :label="$t('table.id')" type="index" align="center" width="80">
                            </el-table-column>
                            <el-table-column :label="$t('table.date')" width="150px" align="center">
                                <template slot-scope="{row}">
                                    <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
                                </template>
                            </el-table-column>
                            <el-table-column :label="$t('permission.apiName')" width="150px" align="center">
                                <template slot-scope="{row}">
                                    <span>{{ row.name }}</span>
                                </template>
                            </el-table-column>
                            <el-table-column :label="$t('permission.permissionCode')" class-name="status-col" width="150">
                                <template slot-scope="{row}">
                                    {{ row.code }}
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
                            <el-table-column :label="$t('table.actions')" align="center" width="200"
                                class-name="small-padding fixed-width">
                                <template slot-scope="{row,$index}">
                                    <el-button v-permission="'system.permission.edit'" type="primary" size="mini" @click="handleUpdate(row)">
                                        {{ $t('table.edit') }}
                                    </el-button>
                                    <el-button v-permission="'system.permission.delete'" size="mini" type="danger"
                                        @click="handleDelete(row, $index)">
                                        {{ $t('table.delete') }}
                                    </el-button>
                                </template>
                            </el-table-column>
                        </el-table>
                    </template>
                </template>
            </el-table-column>
            <el-table-column :label="$t('table.id')" type="index" align="center" width="80">
            </el-table-column>
            <el-table-column :label="$t('table.date')" width="150px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('permission.menuName')" width="150px">
                <template slot-scope="{row}">
                    <div :style="{'padding-left':row.parentId==0?'0px':'15px'}">
                        <i v-if="row.icon && row.icon.indexOf('el-icon') >= 0" :class="row.icon"
                            style="margin: 0;width:18px;" />
                        <svg-icon v-if="row.icon && row.icon.indexOf('el-icon') < 0" :icon-class='row.icon'
                            style="margin: 0;width:18px;" />{{ row.name }}</div>
                </template>
            </el-table-column>
            <el-table-column :label="$t('permission.permissionCode')" class-name="status-col" width="150">
                <template slot-scope="{row}">
                    {{ row.code }}
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
            <el-table-column :label="$t('table.actions')" align="center" width="200"
                class-name="small-padding fixed-width">
                <template slot-scope="{row,$index}">
                    <el-button type="primary" v-permission="'system.permission.edit'" size="mini" @click="handleUpdate(row)">
                        {{ $t('table.edit') }}
                    </el-button>
                    <el-button v-permission="'system.permission.delete'" size="mini" type="danger"
                        @click="handleDelete(row, $index)">
                        {{ $t('table.delete') }}
                    </el-button>
                </template>
            </el-table-column>
            </el-table>
        </el-container>
    </el-container>

    <el-dialog :title="textMap[dialogStatus] + dialogTitle" :visible.sync="dialogFormVisible">
        <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="100px"
            style="width: 450px; margin-left:50px;">
            <el-form-item :label="$t('permission.permissionType')" prop="PermissionTypge">
                <el-radio-group v-model="temp.permissionType" size="large" :disabled="temp.id != undefined" @change="querySearch">
                    <el-radio-button label="菜单" />
                    <el-radio-button label="按钮" />
                    <el-radio-button label="Api" />
                </el-radio-group>
            </el-form-item>
            <el-form-item :label="$t('permission.parentMenu')" prop="parentId">
                <el-autocomplete popper-class="my-autocomplete" v-model="temp.parentName"
                    :fetch-suggestions="querySearch" placeholder="请输入内容" @select="handleSelect">
                    <i class="el-icon-edit el-input__icon" slot="suffix" @click="handleIconClick">
                    </i>
                    <template slot-scope="{ item }">
                        <div :style="{ 'margin-left': (item.parentId == 0 ? 0 : '20' + 'px') }">
                            <i v-if="item.icon && item.icon.indexOf('el-icon') >= 0" :class="item.icon"
                                style="margin: 0;width:18px;" />
                            <svg-icon v-if="item.icon && item.icon.indexOf('el-icon') < 0" :icon-class='item.icon'
                                style="margin: 0;width:18px;" /><span
                                :style="{ 'color': (item.parentId == 0 && temp.permissionType != '菜单' ? '#ccc' : '') }">{{
                                    item.name
                                }}</span>
                        </div>
                    </template>
                </el-autocomplete>
            </el-form-item>
            <el-form-item :label="$t('permission.permissionName')" prop="name">
                <el-input v-model="temp.name" type="text" placeholder="请输入" />
            </el-form-item>
            <el-form-item :label="$t('permission.permissionCode')" prop="code">
                <el-input v-model="temp.code" type="text" placeholder="请输入" />
            </el-form-item>
            <el-form-item :label="$t('permission.icon')" prop="icon">
                <el-input v-model="temp.icon" type="text" placeholder="请输入" />
            </el-form-item>
            <el-form-item :label="$t('table.status')" prop="status">
                <el-radio-group v-model="temp.status" size="large">
                    <el-radio-button label="正常" />
                    <el-radio-button label="禁用" />
                </el-radio-group>
            </el-form-item>
            <el-form-item v-if="temp.permissionType == 'Api'" :label="$t('permission.apiUrl')" prop="url">
                <el-input v-model="temp.url" type="text" placeholder="兼容正则表达式" />
            </el-form-item>
            <el-form-item v-if="temp.permissionType == 'Api'" :label="$t('permission.apiMethod')" prop="apiMethod">
                <el-radio-group v-model="temp.apiMethod" size="large">
                    <el-radio-button label="GET" />
                    <el-radio-button label="POST" />
                    <el-radio-button label="PUT" />
                    <el-radio-button label="DElETE" />
                </el-radio-group>
            </el-form-item>
            <el-form-item :label="$t('table.remark')" prop="remark">
                <el-input v-model="temp.remark" :autosize="{ minRows: 2, maxRows: 4 }" type="textarea"
                    placeholder="请输入" />
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
import { getAllPermissions, updatePermission, deletePermission, createPermission } from '@/api/permission'
import waves from '@/directive/waves' // waves directive
import { parseTime } from '@/utils'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import permission from '@/directive/permission/index'

export default {
    name: 'Permission',
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
            },
            temp: {
                id: undefined,
                permissionName: '',
                fullName: '',
                remark: '',
                status: '正常',
                permissionType: '菜单',
                parentName: '',
                parentId:0,
                url: '',
                apiMethod: ''
            },
            dialogFormVisible: false,
            dialogStatus: '',
            textMap: {
                update: '编辑',
                create: '创建'
            },
            dialogTitle: '',
            rules: {
                parentId: [{ required: true, message: '父节点必选', trigger: 'blur' }],
                name: [{ required: true, message: '角色名必输', trigger: 'blur' }],
                code: [{ required: true, message: '编码必输', trigger: 'blur' }],
                url: [{ required: true, message: 'Api Url必输', trigger: 'blur' }],
                apiMethod: [{ required: true, message: 'Api动作必选', trigger: 'blur' }],
                status: [{ required: true, message: '状态必选', trigger: 'blur' }]
            },
            treeMenus: [],
            tableMenus: [],
            menuOpeneds: []
        }
    },
    created() {
        this.getList()
    },
    methods: {
        getList() {
            this.listLoading = true
            getAllPermissions(this.listQuery).then(response => {
                //菜单
                let list = response
                let menus = []
                // 按照 sort 属性进行排序
                list.sort((a, b) => (a.sort > b.sort||a.parentId < b.parentId) ? 1 : -1)
                for (let i in list) {
                    if (!list[i].parentId) {
                        let menu = list[i]
                        menu.childs = list.filter(x => x.parentId == menu.id);
                        menus.push(menu)
                    }
                }
                this.treeMenus = menus
                let menuOpeneds = []
                for (let i = 0; i < menus.length; i++) {
                    menuOpeneds.push(i + '')
                }
                this.menuOpeneds = menuOpeneds

                //表格
                let tableMenus = []
                for (let i = 0; i < menus.length; i++) {
                    tableMenus.push(menus[i])
                    tableMenus = tableMenus.concat(menus[i].childs)
                }
                this.tableMenus = tableMenus

                this.list = response
                this.total = response.length

                this.listLoading = false
            })
        },
        querySearch(queryString, cb) {
            let results
            switch (this.temp.permissionType) {
                case '按钮':
                    results = this.list.filter(x => x.permissionType == 0 && x.parentId)
                    break;
                case 'Api':
                    results = this.list.filter(x => x.permissionType == 0 && x.parentId)
                    break;
                default:
                    results = this.list.filter(x => !x.parentId)
                    results.unshift({id:0,name:'根目录',icon:'el-icon-s-home',parentId:0})
                    break;
            }
            if(results.length > 0)
            {
                this.temp.parentName = results[0].name
            }
            if(cb)
            {
                cb(results);
            }
        },
        handleSelect(item) {
            this.temp.parentName = item.name
            this.temp.parentId = item.id
        },
        handleIconClick(ev) {
            console.log(ev);
        },
        resetTemp() {
            this.temp = {
                id: undefined,
                permissionName: '',
                fullName: '',
                remark: '',
                status: '正常',
                permissionType: '',
                parentName: '',
                parentId: 0,
                url: '',
                apiMethod: ''
            }
        },
        handleCreate(type, rowId) {
            this.resetTemp()
            this.dialogStatus = 'create'
            switch (type) {
                case 'button':
                    this.dialogTitle = '按钮/功能权限'
                    this.temp.permissionType = '按钮'
                    this.temp.parentId = rowId
                    break;
                case 'api':
                    this.dialogTitle = 'Api权限'
                    this.temp.permissionType = 'Api'
                    this.temp.apiMethod = 'Get'
                    this.temp.parentId = rowId
                    break;
                default:
                    this.dialogTitle = '菜单'
                    this.temp.permissionType = '菜单'
                    break
            }
            this.temp.parentName = !this.temp.parentId ? '根目录' : this.list.find(x=>x.id == this.temp.parentId).name;
            this.dialogFormVisible = true
            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })
        },
        createData() {
            this.$refs['dataForm'].validate((valid) => {
                if (valid) {
                    let tempData = Object.assign({}, this.temp)
                    tempData.status = tempData.status == '正常' ? 1 : 0
                    switch (tempData.permissionType) {
                        case '菜单':
                            tempData.permissionType = 0;
                            break;
                        case '按钮':
                            tempData.permissionType = 1;
                            break;
                        case 'Api':
                            tempData.permissionType = 2;
                            break;
                    }
                    tempData.parentId = tempData.parentId == 0 ? '' : tempData.parentId
                    createPermission(tempData).then(() => {
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
            this.temp.status = this.temp.status == 1 ? '正常' : '禁用'
            this.temp.parentId = !this.temp.parentId ? 0 : this.temp.parentId;
            switch (this.temp.permissionType) {
                case 0:
                    this.temp.permissionType = '菜单'
                    break;
                case 1:
                    this.temp.permissionType = '按钮'
                    break;
                case 2:
                    this.temp.permissionType = 'Api'
                    break;
            }
            this.temp.parentName = this.temp.parentId == 0 ? '根目录' : this.list.find(x=>x.id == this.temp.parentId).name;
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
                    switch (tempData.permissionType) {
                        case '菜单':
                            tempData.permissionType = 0;
                            break;
                        case '按钮':
                            tempData.permissionType = 1;
                            break;
                        case 'Api':
                            tempData.permissionType = 2;
                            break;
                    }
                    tempData.parentId = tempData.parentId == 0 ? '' : tempData.parentId
                    updatePermission(tempData).then(() => {
                        this.temp.status = this.temp.status == '正常' ? 1 : 0
                        this.getList();
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
            this.$confirm('确认删除此权限?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                deletePermission(row.id).then((response) => {
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
                        this.getList()
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