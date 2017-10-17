using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Lucy.WebApi
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            //添加全局权限
            config.Filters.Add(new AuthorizeAttribute());
            //跨域配置
            //config.EnableCors(new EnableCorsAttribute(string.Format("{0}", YQSH.InformationPortalMgmt.Constants.MVCURL), "accept, authorization", "GET", "WWW-Authenticate"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            return config;
        }

    }
}
