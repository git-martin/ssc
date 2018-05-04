using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace ErpMongoLog
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var jsonSetting = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            //忽略循环引用
            jsonSetting.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //日期格式化为yyyy-MM-dd HH:mm:ss
            //jsonSetting.Converters.Insert(0, new StandardJsonDateTimeConverter());
            jsonSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //允许跨站访问
            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // 对 JSON 数据使用混合大小写。
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
