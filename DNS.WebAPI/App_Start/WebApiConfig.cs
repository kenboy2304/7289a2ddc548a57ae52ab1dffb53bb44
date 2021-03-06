﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DNS.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
            config.Routes.MapHttpRoute(
                name: "Sitemap",
                routeTemplate: "api/sitemap.xml",
                defaults: new { controller = "System", action = "SiteMap" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
