<template>
    <div class="app-container" style="height:90%;">
        <el-container style="border: 1px solid #eee;">
            <el-aside width="250px" style="background-color: rgb(238, 241, 246);margin-bottom: 0;padding-bottom: 20px;">
                <div style="padding-bottom:10px ;1">
                    <el-row>
                        <el-col :span="10" :offset="2"><strong>角色列表</strong></el-col>
                    </el-row>
                </div>
                <el-menu default-active="0" @select="roleSelect">
                    <template v-for="(role, index) in roles">
                        <el-menu-item :index="index + ''">
                            <svg-icon icon-class='peoples' style="margin: 0;width:18px;" />
                            <span>{{ role.name }}</span>
                        </el-menu-item>
                    </template>
                </el-menu>
            </el-aside>
            <el-table v-loading="listLoading" :data="tableMenus" border fit highlight-current-row style="width: 100%;">
                <el-table-column :label="$t('rolePermission.menuName')" width="150px">
                    <template slot-scope="{row,$index}">
                        <div :style="{ 'padding-left': row.parentId == 0 ? '0px' : '15px' }">
                            <el-checkbox v-model="checkedList[row.id + '']" :indeterminate="indeterminates[row.id + '']"
                                @change="checkChange(row.id)" size="large" style="margin-right: 5px;"></el-checkbox>
                            <i v-if="row.icon && row.icon.indexOf('el-icon') >= 0" :class="row.icon"
                                style="margin: 0;width:18px;" />
                            <svg-icon v-if="row.icon && row.icon.indexOf('el-icon') < 0" :icon-class='row.icon'
                                style="margin: 0;width:18px;" />{{ row.name }}
                        </div>
                    </template>
                </el-table-column>
                <el-table-column :label="$t('rolePermission.elementName')">
                    <template slot-scope="{row,$index}">
                        <el-checkbox
                            v-for="(item, index) in list.filter(x => x.parentId == row.id && x.permissionType == 1)"
                            v-model="checkedList[row.id + '$1$' + item.id]" @change="checkChange(row.id, 1, item.id)"
                            :label="item.name" size="large" border style="margin-right: 10px;" />
                    </template>
                </el-table-column>
                <el-table-column :label="$t('rolePermission.apiName')">
                    <template slot-scope="{row,$index}">
                        <el-checkbox
                            v-for="(item, index) in list.filter(x => x.parentId == row.id && x.permissionType == 2)"
                            v-model="checkedList[row.id + '$2$' + item.id]" @change="checkChange(row.id, 2, item.id)"
                            :label="item.name" size="large" border style="margin-right: 10px;" />
                    </template>
                </el-table-column>
            </el-table>
        </el-container>
        <div style="clear:both;text-align: center;padding-top: 20px;">
            <el-button v-permission="'system.rolepermission.update'" v-waves type="primary" size="large" @click="savePermissions">保存</el-button>
        </div>
    </div>
</template>

<script>
import { getAllPermissions, } from '@/api/permission'
import { getAllRoles, getPermissions, savePermissions } from '@/api/role'
import waves from '@/directive/waves' // waves directive
import permission from '@/directive/permission/index'

export default {
    name: 'RolePermission',
    directives: { waves, permission },
    data() {
        return {
            list: null,
            listLoading: true,
            tableMenus: [],
            roles: [],
            checkedList: {}
        }
    },
    created() {
        this.getPermissionList()
        this.getRoleList()
    },
    methods: {
        checkChange(rowId, type, childId) {
            let checkedList = this.checkedList;
            let tableMenus = this.tableMenus
            if (!type) {
                let menu = tableMenus.find(x => x.id == rowId)
                if (menu.parentId == 0) {
                    //同步子菜单的选择框的值
                    let childs = tableMenus.filter(x => x.parentId == rowId)
                    for (let key in childs) {
                        checkedList[childs[key].id + ''] = checkedList[rowId + '']
                    }
                    for (let key in childs) {
                        this.checkChange(childs[key].id)
                    }
                }
                else {
                    //遍历功能、API权限，并同步选择的值
                    for (let key in checkedList) {
                        if (key.indexOf('$') > 0 && key.split('$')[0] == rowId) {
                            checkedList[key] = checkedList[rowId + '']
                        }
                    }
                }
            }
        },
        getPermissionList() {
            this.listLoading = true
            getAllPermissions().then(response => {
                //菜单
                let list = response
                let menus = []
                let checkedList = {}
                let indeterminates = {}
                
                // 按照 sort 属性进行排序
                list.sort((a, b) => (a.sort > b.sort) ? 1 : -1)
                for (let i in list) {
                    if (!list[i].parentId) {
                        //菜单
                        let menu = list[i]
                        let childs = list.filter(x => x.parentId == menu.id)
                        menu.childs = childs
                        menus.push(menu)

                        //初始化复选框
                        checkedList[menu.id + ''] = false
                        indeterminates[menu.id + ''] = false
                        for (let childIndex = 0; childIndex < childs.length; childIndex++) {
                            let childId = childs[childIndex].id
                            checkedList[childId + ''] = false
                            indeterminates[childId + ''] = false

                            //获取功能/按钮列表
                            let elementList = list.filter(x => x.parentId == childId && x.permissionType == 1)
                            for (let index = 0; index < elementList.length; index++) {
                                checkedList[childId + '$1$' + elementList[index].id] = false
                            }

                            //获取Api列表
                            let apiList = list.filter(x => x.parentId == childId && x.permissionType == 2)
                            for (let index = 0; index < apiList.length; index++) {
                                checkedList[childId + '$2$' + apiList[index].id] = false
                            }
                        }
                    }
                }
                this.checkedList = checkedList
                this.indeterminates = indeterminates

                //表格
                let tableMenus = []
                for (let i = 0; i < menus.length; i++) {
                    tableMenus.push(menus[i])
                    tableMenus = tableMenus.concat(menus[i].childs)
                }
                this.tableMenus = tableMenus
                this.list = response

                this.listLoading = false
            })
        },
        getRoleList() {
            this.listLoading = true
            getAllRoles().then(response => {
                this.roles = response
                if (this.roles.length > 0) {
                    this.roleSelect(0)
                }
                this.listLoading = false
            })
        },
        getRolePermissions(id) {
            this.listLoading = true
            //获取某个角色包含的权限列表
            getPermissions(id).then(response => {
                let permissions = response
                let checkedList = this.checkedList
                for(var i in checkedList)
                {
                    checkedList[i] = false
                }
                for (var i in permissions) {
                    let item = permissions[i]
                    if (!item.parentId || item.permissionType == 0) {
                        checkedList[item.id + ''] = true
                    }
                    else if (item.permissionType == 1) {
                        checkedList[item.parentId + '$1$' + item.id] = true
                    }
                    else if (item.permissionType == 2) {
                        checkedList[item.parentId + '$2$' + item.id] = true
                    }
                }
                this.listLoading = false
            })
        },
        roleSelect(index) {
            this.rootId = this.roles[index].id
            this.getRolePermissions(this.rootId)
        },
        savePermissions() {
            let checkedList = this.checkedList
            let permissionIds = []
            for(let i in checkedList)
            {
                if(checkedList[i])
                {
                    if(i.indexOf('$') > 0)
                    {
                        permissionIds.push(i.split('$')[2])
                    }
                    else{
                        permissionIds.push(i.split('$')[0])
                    }
                }
            }
            savePermissions(this.rootId, permissionIds).then(response => {
                if (!response) {
                    this.$notify({
                        title: '成功',
                        message: '更新成功',
                        type: 'success',
                        duration: 2000
                    })
                }
                else {
                    this.$notify({
                        title: '失败',
                        message: '更新失败',
                        type: 'info',
                        duration: 2000
                    })
                }
            })
        }
    }
}
</script>