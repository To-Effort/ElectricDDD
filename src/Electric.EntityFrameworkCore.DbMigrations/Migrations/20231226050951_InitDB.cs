using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Electric.EntityFrameworkCore.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EleAuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Method = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReturnValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    AuditLogType = table.Column<int>(type: "int", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleAuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Component = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionType = table.Column<int>(type: "int", nullable: false),
                    ApiMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElePermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EleRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EleUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EleRoleClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EleRoleClaim_EleRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EleRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EleRolePermission",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleRolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_EleRolePermission_ElePermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ElePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EleRolePermission_EleRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EleRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EleUserClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EleUserClaim_EleUser_UserId",
                        column: x => x.UserId,
                        principalTable: "EleUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EleUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUserLogin", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_EleUserLogin_EleUser_UserId",
                        column: x => x.UserId,
                        principalTable: "EleUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EleUserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_EleUserRole_EleRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EleRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EleUserRole_EleUser_UserId",
                        column: x => x.UserId,
                        principalTable: "EleUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EleUserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_EleUserToken_EleUser_UserId",
                        column: x => x.UserId,
                        principalTable: "EleUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ElePermission",
                columns: new[] { "Id", "ApiMethod", "Code", "Component", "CreationTime", "CreatorId", "Icon", "LastModificationTime", "LastModifierId", "Name", "ParentId", "PermissionType", "Remark", "Sort", "Status", "Url" },
                values: new object[,]
                {
                    { new Guid("0e216a44-c4e1-44db-bb02-a6dc36cd77ef"), "GET", "system.permission.edit", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(2400), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "编辑", new Guid("e8b95a50-7078-487f-9d05-19caec620948"), 1, null, 0, 1, null },
                    { new Guid("2ee7bc30-d4b6-47e1-b38d-6b7cb63d7b99"), "GET", "system", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(940), new Guid("00000000-0000-0000-0000-000000000000"), "el-icon-s-tools", null, new Guid("00000000-0000-0000-0000-000000000000"), "系统管理", null, 0, null, 0, 1, null },
                    { new Guid("3985168e-0ff0-40bf-a076-1849c0e04894"), "GET", "log.auditlog", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(1035), new Guid("00000000-0000-0000-0000-000000000000"), "bug", null, new Guid("00000000-0000-0000-0000-000000000000"), "日志管理", null, 0, null, 0, 1, null },
                    { new Guid("3a631953-c258-4280-8df8-515ac86c73a2"), "GET", "system.rolepermission", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(1022), new Guid("00000000-0000-0000-0000-000000000000"), "example", null, new Guid("00000000-0000-0000-0000-000000000000"), "角色权限", new Guid("2ee7bc30-d4b6-47e1-b38d-6b7cb63d7b99"), 0, null, 0, 1, null },
                    { new Guid("48aac212-bb22-49b7-ac70-7830379ec0f8"), "GET", "system.role.delete", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(2390), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "删除", new Guid("a84d1073-e869-4a0f-801b-37355d074683"), 1, null, 0, 1, null },
                    { new Guid("4cb4bbec-2e60-4b3e-94c8-d409acb9cd41"), "GET", "system.user.add", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(1041), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "添加", new Guid("95e211a2-c72b-4c02-a383-4eb710e552d2"), 1, null, 0, 1, null },
                    { new Guid("8e6196ba-e4a1-4e33-b1b6-a8131478d27c"), "GET", "system.user.edit", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(1045), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "编辑", new Guid("95e211a2-c72b-4c02-a383-4eb710e552d2"), 1, null, 0, 1, null },
                    { new Guid("95e211a2-c72b-4c02-a383-4eb710e552d2"), "GET", "system.user", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(1009), new Guid("00000000-0000-0000-0000-000000000000"), "el-icon-user-solid", null, new Guid("00000000-0000-0000-0000-000000000000"), "用户管理", new Guid("2ee7bc30-d4b6-47e1-b38d-6b7cb63d7b99"), 0, null, 0, 1, null },
                    { new Guid("970f60c7-ea07-4efb-874d-79204f1d1655"), "GET", "system.permission.add", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(2396), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "添加", new Guid("e8b95a50-7078-487f-9d05-19caec620948"), 1, null, 0, 1, null },
                    { new Guid("a84d1073-e869-4a0f-801b-37355d074683"), "GET", "system.role", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(1014), new Guid("00000000-0000-0000-0000-000000000000"), "peoples", null, new Guid("00000000-0000-0000-0000-000000000000"), "角色管理", new Guid("2ee7bc30-d4b6-47e1-b38d-6b7cb63d7b99"), 0, null, 0, 1, null },
                    { new Guid("cf114614-e73c-413f-b455-ace9b1230099"), "GET", "system.role.add", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(2368), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "添加", new Guid("a84d1073-e869-4a0f-801b-37355d074683"), 1, null, 0, 1, null },
                    { new Guid("d4ebab18-76fe-4546-9ab2-109f96f01918"), "GET", "system.role.edit", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(2386), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "编辑", new Guid("a84d1073-e869-4a0f-801b-37355d074683"), 1, null, 0, 1, null },
                    { new Guid("d9cdd628-53af-45cf-95e1-9a1a714adffc"), "GET", "system.rolepermission.update", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(2409), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "更新", new Guid("3a631953-c258-4280-8df8-515ac86c73a2"), 1, null, 0, 1, null },
                    { new Guid("e6abe7c7-6f3a-4480-b9d9-80b223ef6a16"), "GET", "system.permission.delete", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(2405), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "删除", new Guid("e8b95a50-7078-487f-9d05-19caec620948"), 1, null, 0, 1, null },
                    { new Guid("e8b95a50-7078-487f-9d05-19caec620948"), "GET", "system.permission", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(1018), new Guid("00000000-0000-0000-0000-000000000000"), "list", null, new Guid("00000000-0000-0000-0000-000000000000"), "菜单管理", new Guid("2ee7bc30-d4b6-47e1-b38d-6b7cb63d7b99"), 0, null, 0, 1, null },
                    { new Guid("efb1dedb-d98f-4c0e-aabf-54ba9264538c"), "GET", "system.user.delete", null, new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(1713), new Guid("00000000-0000-0000-0000-000000000000"), null, null, new Guid("00000000-0000-0000-0000-000000000000"), "删除", new Guid("95e211a2-c72b-4c02-a383-4eb710e552d2"), 1, null, 0, 1, null }
                });

            migrationBuilder.InsertData(
                table: "EleRole",
                columns: new[] { "Id", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "LastModifierId", "Name", "NormalizedName", "Remark", "Status" },
                values: new object[] { new Guid("388bc057-97f4-4868-b864-d104541b52fa"), "b7b8c8a22f1b46739f363c9ed8eb1651", new DateTime(2023, 12, 26, 13, 9, 51, 467, DateTimeKind.Local).AddTicks(3927), new Guid("00000000-0000-0000-0000-000000000000"), null, new Guid("00000000-0000-0000-0000-000000000000"), "管理员", "管理员", null, 1 });

            migrationBuilder.InsertData(
                table: "EleUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreationTime", "CreatorId", "Email", "EmailConfirmed", "FullName", "LastModificationTime", "LastModifierId", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Remark", "SecurityStamp", "Status", "UserName" },
                values: new object[] { new Guid("6fd517ac-ac8b-4a38-aefb-487c50bb2aca"), 0, "25bfc1b7132d478fa191bb0ba9247d6f", new DateTime(2023, 12, 26, 13, 9, 51, 467, DateTimeKind.Local).AddTicks(4528), new Guid("00000000-0000-0000-0000-000000000000"), "admin@eletric.com", true, "管理员", null, new Guid("00000000-0000-0000-0000-000000000000"), null, "ADMIN@ELETRIC.COM", "ADMIN", "AQAAAAIAAYagAAAAEI0+KF1TsaGvc2wIEFeTbgnMFnQItHN4v6Z31SJP5VHz5F1tEozu0sv7s8eNKOlPRA==", null, false, null, "b350f12c-eb84-443d-9cfa-d1f04a95438b", 1, "admin" });

            migrationBuilder.InsertData(
                table: "EleRolePermission",
                columns: new[] { "PermissionId", "RoleId", "CreationTime", "CreatorId" },
                values: new object[,]
                {
                    { new Guid("0e216a44-c4e1-44db-bb02-a6dc36cd77ef"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3170), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("2ee7bc30-d4b6-47e1-b38d-6b7cb63d7b99"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3094), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("3985168e-0ff0-40bf-a076-1849c0e04894"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3127), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("3a631953-c258-4280-8df8-515ac86c73a2"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3124), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("48aac212-bb22-49b7-ac70-7830379ec0f8"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3137), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4cb4bbec-2e60-4b3e-94c8-d409acb9cd41"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3128), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8e6196ba-e4a1-4e33-b1b6-a8131478d27c"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3130), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("95e211a2-c72b-4c02-a383-4eb710e552d2"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3119), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("970f60c7-ea07-4efb-874d-79204f1d1655"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3156), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a84d1073-e869-4a0f-801b-37355d074683"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3121), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cf114614-e73c-413f-b455-ace9b1230099"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3134), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d4ebab18-76fe-4546-9ab2-109f96f01918"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3136), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d9cdd628-53af-45cf-95e1-9a1a714adffc"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3174), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e6abe7c7-6f3a-4480-b9d9-80b223ef6a16"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3172), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e8b95a50-7078-487f-9d05-19caec620948"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3123), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("efb1dedb-d98f-4c0e-aabf-54ba9264538c"), new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(3131), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EleUserRole",
                columns: new[] { "RoleId", "UserId", "CreationTime", "CreatorId" },
                values: new object[] { new Guid("388bc057-97f4-4868-b864-d104541b52fa"), new Guid("6fd517ac-ac8b-4a38-aefb-487c50bb2aca"), new DateTime(2023, 12, 26, 13, 9, 51, 635, DateTimeKind.Local).AddTicks(807), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_EleAuditLog_CreationTime",
                table: "EleAuditLog",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_ElePermission_CreationTime",
                table: "ElePermission",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_EleRole_CreationTime",
                table: "EleRole",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_EleRole_NormalizedName",
                table: "EleRole",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_EleRoleClaim_RoleId",
                table: "EleRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EleRolePermission_PermissionId",
                table: "EleRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EleRolePermission_RoleId_PermissionId",
                table: "EleRolePermission",
                columns: new[] { "RoleId", "PermissionId" });

            migrationBuilder.CreateIndex(
                name: "IX_EleUser_CreationTime",
                table: "EleUser",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_EleUser_Email",
                table: "EleUser",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_EleUser_NormalizedEmail",
                table: "EleUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_EleUser_NormalizedUserName",
                table: "EleUser",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_EleUser_UserName",
                table: "EleUser",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_EleUserClaim_UserId",
                table: "EleUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EleUserLogin_LoginProvider_ProviderKey",
                table: "EleUserLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_EleUserRole_RoleId_UserId",
                table: "EleUserRole",
                columns: new[] { "RoleId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EleAuditLog");

            migrationBuilder.DropTable(
                name: "EleRoleClaim");

            migrationBuilder.DropTable(
                name: "EleRolePermission");

            migrationBuilder.DropTable(
                name: "EleUserClaim");

            migrationBuilder.DropTable(
                name: "EleUserLogin");

            migrationBuilder.DropTable(
                name: "EleUserRole");

            migrationBuilder.DropTable(
                name: "EleUserToken");

            migrationBuilder.DropTable(
                name: "ElePermission");

            migrationBuilder.DropTable(
                name: "EleRole");

            migrationBuilder.DropTable(
                name: "EleUser");
        }
    }
}
