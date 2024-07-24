using Electric.Domain.Entitys.Identity;
using Electric.Domain.Manager;
using Electric.Domain.Manager.Identity;
using Electric.Domain.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Electric.Domain.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 领域注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddDomain(this IServiceCollection services)
        {
            //注入UserManager
            services.TryAddScoped(typeof(UserManager));
            services.TryAddScoped(typeof(UserManager<EleUser>), provider => provider.GetService(typeof(UserManager)));

            //注入UserStore
            services.TryAddScoped(typeof(UserStore));
            services.TryAddScoped(typeof(IUserStore<EleUser>), provider => provider.GetService(typeof(UserStore)));

            //注入RoleManager
            services.TryAddScoped<RoleManager>();
            services.TryAddScoped(typeof(RoleManager<EleRole>), provider => provider.GetService(typeof(RoleManager)));

            //注入RoleStore
            services.TryAddScoped<RoleStore>();
            services.TryAddScoped(typeof(IRoleStore<EleRole>), provider => provider.GetService(typeof(RoleStore)));

            //注入PermissionManager
            //services.TryAddScoped(typeof(PermissionManager));

            //IDomainService注入
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                List<Type> types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsGenericType && t.GetInterfaces().Contains(typeof(IDomainService)))
                .ToList();

                types.ForEach(impl =>
                {
                    //获取该类所继承的所有接口
                    Type[] interfaces = impl.GetInterfaces();
                    interfaces.ToList().ForEach(i =>
                    {
                        services.AddScoped(i, impl);
                    });
                    services.AddScoped(impl);
                });
            }

            //配置Identity的用户和角色
            services.AddIdentityCore<EleUser>().AddRoles<EleRole>().AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>();
        }
    }
}
