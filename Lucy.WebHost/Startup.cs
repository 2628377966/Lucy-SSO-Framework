using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Serilog;
using System.IO;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Host.Config;
using IdentityServer3.Core.Services;
using Lucy.WebHost.UserService;
using Configuration;

[assembly: OwinStartup(typeof(Lucy.WebHost.Startup))]

namespace Lucy.WebHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.RollingFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Logs\log-{Date}.txt"))
               .CreateLogger();

            var factory = new IdentityServerServiceFactory()
                        .UseInMemoryClients(Clients.Get())
                        .UseInMemoryScopes(Scopes.Get());

            factory.UserService = new Registration<IUserService, CustomUserService>();//自定义的登录验证
            factory.ConfigureUserServiceCache(TimeSpan.FromDays(1)); //过期时间1天
            var options = new IdentityServerOptions
            {
                SigningCertificate = Certificate.Load(),
                Factory = factory,
                RequireSsl = false,
                AuthenticationOptions = new AuthenticationOptions
                {
                    EnablePostSignOutAutoRedirect = true//登出后自动重定向
                },
                EventsOptions = new EventsOptions
                {
                    RaiseSuccessEvents = true,
                    RaiseErrorEvents = true,
                    RaiseFailureEvents = true,
                    RaiseInformationEvents = true
                }
                
            };

            app.Map("/core", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(options);
            });
        }
    }
}
