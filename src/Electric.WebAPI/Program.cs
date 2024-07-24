using Electric.API;
using Electric.API.Extensions;
using Electric.Application.Auth;
using Electric.Application.DependencyInjection;
using Electric.Domain.DependencyInjection;
using Electric.WebAPI.Extensions;
using Electric.SqlSugarCore.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//ʱ���ʽ
builder.Services.AddControllers().AddNewtonsoftJson(option =>
{
    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//������־
builder.Logging.AddLog4Net();

// Swagger/OpenAPI ����
builder.AddElectricSwaggerGen();

//��������
builder.AddElectricCorsOrigins();

//Applicationע��
builder.Services.AddApplication();

//JWT
var jwtBearer = builder.Configuration.GetSection(AppSettings.Authentication).GetSection(AppSettings.JwtBearer);
var jwtBearerSetting = new JwtBearerSetting(
    jwtBearer.GetValue<string>(AppSettings.Issuer),
    jwtBearer.GetValue<string>(AppSettings.Audience),
    jwtBearer.GetValue<string>(AppSettings.SecurityKey)
    );
builder.Services.AddJWT(jwtBearerSetting);

//�����ע��
builder.Services.AddDomain();

//EntityFrameworkCoreע��
var provider = builder.Configuration.GetValue<string>("DataProvider");
var connection = string.Empty;
//���õ����ݿ�����
switch (provider)
{
    case "MsSql":
        connection = builder.Configuration.GetConnectionString("MsSqlConnection") ?? throw new InvalidOperationException("MsSqlConnection��appsettings.jsonδ����");
        break;
    case "MySql":
        connection = builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("MySqlConnection��appsettings.jsonδ����");
        //MySql��Ҫ����汾��ServerVersion.AutoDetect���������ַ����Զ���ȡ
        break;
}

//ʹ��EF ORM
//builder.Services.AddEntityFrameworkCore(provider == "MySql" ? DbType.MySql : DbType.SqlServer, connection);

//ʹ��SqlSugarCore
builder.Services.AddSqlSugarCore(provider == "MySql" ? SqlSugar.DbType.MySql : SqlSugar.DbType.SqlServer, connection);

//�����������
builder.AddRateLimiter();

//���û���
builder.Services.AddMemoryCache();

var app = builder.Build();

//API��������
app.UseRateLimiter();

//��������
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Ӧ�÷����ṩ��
app.UseServiceProvider();

app.UseHttpsRedirection();

//���������֤
app.UseAuthentication();
app.UseAuthorization();

//����body����
app.Use(next => context =>
{
    context.Request.EnableBuffering();
    return next(context);
});

app.MapControllers();

app.Run();
