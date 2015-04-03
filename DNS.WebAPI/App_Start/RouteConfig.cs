using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;

namespace IdentitySample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index"}
            );
            routes.MapRoute(
                name: "Page",
                url: "{name}.html",
                defaults: new { controller = "Article", action = "Page", name = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "BibleDaily",
                url: "lich-doc-kinh-thanh-thang-{month}",
                defaults: new { controller = "Article", action = "BibleDaily", month = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Daily",
                url: "bai-giang-ngay-{date}",
                defaults: new { controller = "Article", action = "Daily", date = UrlParameter.Optional }
            );
           
            routes.MapRoute(
                name: "Article2",
                url: "{catalog}/{name}.dns",
                defaults: new { controller = "Article", action = "Index",catalog =UrlParameter.Optional, name = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Article1",
               url: "{name}.dns",
               defaults: new { controller = "Article", action = "Index", name = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Catalog",
                url: "{name}/",
                defaults: new { controller = "Catalog", action = "Index", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}