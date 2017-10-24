using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IdentityModel.Tokens;
using IdentityServer3.AccessTokenValidation;
using Autofac;
using System.Reflection;
using Autofac.Integration.WebApi;
using System.Web.Http;

[assembly: OwinStartup(typeof(Lucy.WebApi.Startup))]

namespace Lucy.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = Constants.BaseAddress,//只接受identityserver的令牌
                RequiredScopes = new[] { Constants.WebApi_Scope },//只接受给generalapi的令牌

                ClientId = Constants.WebApi_CLIENTID,
                ClientSecret = Constants.WebApi_SECRET
            });

            //autofac.webapi2.owin集成 
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).PropertiesAutowired();//注册api容器的实现
            Assembly[] assemblies = new Assembly[] { Assembly.Load("Lucy.Services") };
            builder.RegisterAssemblyTypes(assemblies)
            .Where(type => type.Name.EndsWith("Svc") && !type.IsAbstract)
            .AsImplementedInterfaces().PropertiesAutowired();//注册实现类
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(WebApiConfig.Register(config));
        }
    }
}
