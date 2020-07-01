using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KKSoftWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            config.EnableCors();//配置跨域

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //映射htp路由
            config.Routes.MapHttpRoute(
                name: "DefaultApi",//一个项目中不允许重复
                routeTemplate: "api/{controller}/{id}",//路由模板
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
