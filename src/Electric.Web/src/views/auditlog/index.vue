<template>
    <div class="app-container">
        <div class="filter-container">
            <el-form :inline="true">
                <el-form-item :label="$t('auditLog.apiUrl') + ':'">
                    <el-input v-model="listQuery.apiUrl" :placeholder="$t('auditLog.apiUrl')" style="width: 150px;"
                        class="filter-item" @keyup.enter.native="handleFilter" />
                </el-form-item>
                <el-form-item :label="$t('auditLog.clientIpAddress') + ':'">
                    <el-input v-model="listQuery.clientIpAddress" :placeholder="$t('auditLog.clientIpAddress')"
                        style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
                </el-form-item>
                <el-form-item :label="$t('auditLog.creatorId') + ':'">
                    <el-select v-model="listQuery.creatorId" clearable filterable :placeholder="$t('auditLog.creatorId')"
                        style="width:120px;">
                        <el-option v-for="item in allUsers" :key="item.id" :label="item.userName" :value="item.id">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item :label="$t('auditLog.startTime') + ':'">
                    <el-date-picker v-model="listQuery.timeRange" type="datetimerange" range-separator="~"
                        :start-placeholder="$t('auditLog.startTime')" :end-placeholder="$t('auditLog.endTime')">
                    </el-date-picker>
                </el-form-item>
                <el-form-item :label="$t('auditLog.auditLogType') + ':'">
                    <el-checkbox-group v-model="listQuery.auditLogTypes">
                        <el-checkbox label="0">正常日志</el-checkbox>
                        <el-checkbox label="99">异常日志</el-checkbox>
                    </el-checkbox-group>
                </el-form-item>
                <el-form-item>
                    <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search"
                        @click="handleFilter">
                        {{ $t('table.search') }}
                    </el-button>
                </el-form-item>
            </el-form>
        </div>

        <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row
            style="width: 100%;">
            <el-table-column :label="$t('table.id')" type="index" align="center" width="80">
            </el-table-column>
            <el-table-column :label="$t('table.date')" width="150px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('auditLog.apiUrl')" width="150px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.apiUrl }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('auditLog.method')" width="80px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.method }}</span>
                </template>
            </el-table-column>
            <el-table-column :show-overflow-tooltip="true" :label="$t('auditLog.parameters')" width="150px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.parameters }}</span>
                </template>
            </el-table-column>
            <el-table-column :show-overflow-tooltip="true" :label="$t('auditLog.returnValue')" width="150px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.exceptionMessage ? "异常" : "OK" }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('auditLog.executionDuration') + '(ms)'" width="120px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.executionDuration }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('auditLog.clientIpAddress')" width="150px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.clientIpAddress }}</span>
                </template>
            </el-table-column>
            <el-table-column :show-overflow-tooltip="true" :label="$t('auditLog.browserInfo')" width="150px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.browserInfo }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('auditLog.auditLogType')" class-name="status-col" width="100">
                <template slot-scope="{row}">
                    <el-tag :type="row.auditLogType | auditLogTypeFilter">
                        {{ row.auditLogType | auditLogTypeTextFilter }}
                    </el-tag>
                </template>
            </el-table-column>
            <el-table-column :label="$t('auditLog.creatorUserName')" width="110px" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.creatorUserName }}</span>
                </template>
            </el-table-column>
            <el-table-column :show-overflow-tooltip="true" :label="$t('auditLog.exceptionMessage')" align="center">
                <template slot-scope="{row}">
                    <span>{{ row.exceptionMessage }}</span>
                    <div>{{ row.exception }}</div>
                </template>
            </el-table-column>
            <el-table-column :label="$t('table.actions')" align="center" width="200" class-name="small-padding fixed-width">
                <template slot-scope="{row,$index}">
                    <el-button size="mini" @click="handleDetail(row)">
                        {{ $t('table.detail') }}
                    </el-button>
                    <el-button size="mini" type="danger" @click="handleDelete(row, $index)">
                        {{ $t('table.delete') }}
                    </el-button>
                </template>
            </el-table-column>
        </el-table>
        <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.prePage"
            @pagination="getList" />

        <el-dialog title="日志详情" :visible.sync="dialogFormVisible">
            <el-form ref="dataForm" :model="temp" label-position="left" label-width="120px"
                style="width: 800px; margin-left:50px;" class="detail-form">
                <el-form-item :label="$t('table.id') + ':'">
                    <span>{{ temp.id }}</span>
                </el-form-item>
                <el-form-item :label="$t('table.date') + ':'">
                    <span>{{ temp.creationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.apiUrl') + ':'">
                    <span>{{ temp.apiUrl }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.method') + ':'">
                    <span>{{ temp.method }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.parameters') + ':'">
                    <span>{{ temp.parameters }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.returnValue') + ':'">
                    <span>{{ temp.exceptionMessage ? "异常" : "OK" }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.executionDuration') + '(ms)：'">
                    <span>{{ temp.executionDuration }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.clientIpAddress') + ':'">
                    <span>{{ temp.clientIpAddress }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.browserInfo') + ':'">
                    <span>{{ temp.browserInfo }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.auditLogType') + ':'">
                    <el-tag :type="temp.auditLogType | auditLogTypeFilter">
                        {{ temp.auditLogType | auditLogTypeTextFilter }}
                    </el-tag>
                </el-form-item>
                <el-form-item :label="$t('auditLog.creatorUserName') + ':'">
                    <span>{{ temp.creatorUserName }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.exceptionMessage') + ':'">
                    <span>{{ temp.exceptionMessage }}</span>
                </el-form-item>
                <el-form-item :label="$t('auditLog.exception') + ':'">
                    <div>{{ temp.exception }}</div>
                </el-form-item>
            </el-form>

            <div slot="footer" class="dialog-footer">
                <el-button type="primary" @click="dialogFormVisible = false">
                    {{ $t('table.confirm') }}
                </el-button>
            </div>
        </el-dialog>
    </div>
</template>
<script>
import { queryAllUsers } from '@/api/user'
import { queryAuditLogs, deleteAuditLog } from '@/api/auditlog'
import waves from '@/directive/waves' // waves directive
import { parseTime } from '@/utils'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
    name: 'User',
    components: { Pagination },
    directives: { waves },
    filters: {
        auditLogTypeFilter(status) {
            const statusMap = {
                '0': 'succ',
                '99': 'danger'
            }
            return statusMap[status]
        },
        auditLogTypeTextFilter(status) {
            const statusMap = {
                '0': '正常',
                '99': '异常'
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
                auditLogTypes: []
            },
            dialogFormVisible: false,
            temp: {},
            allUsers: []
        }
    },
    created() {
        this.getList()
        this.getAllUsers()
    },
    methods: {
        getList() {
            this.listLoading = true
            let timeRange = this.listQuery.timeRange
            if (timeRange) {
                this.listQuery.startTime = parseTime(timeRange[0])
                this.listQuery.endTime = parseTime(timeRange[1])
            }
            else {
                this.listQuery.startTime = ''
                this.listQuery.endTime = ''
            }
            queryAuditLogs(this.listQuery).then(response => {
                this.list = response.auditLogs
                this.total = response.total

                this.listLoading = false
            })
        },
        getAllUsers() {
            queryAllUsers().then(response => {
                this.allUsers = response
            })
        },
        handleFilter() {
            this.listQuery.page = 1
            this.getList()
        },
        handleDetail(row) {
            this.dialogFormVisible = true;
            this.temp = row;
        },
        handleDelete(row, index) {
            this.$confirm('确认删除此日志?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                deleteAuditLog(row.id).then((response) => {
                    this.$notify({
                        title: '成功',
                        message: '删除成功',
                        type: 'success',
                        duration: 2000
                    })
                    this.list.splice(index, 1)
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