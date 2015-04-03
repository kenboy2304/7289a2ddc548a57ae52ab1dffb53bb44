using System.Web.Mvc;

namespace DNS.WebAPI.Areas.DNSAdmin
{
    public class DNSAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DNSAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DNSAdmin_default",
                "DNSAdmin/{controller}/{action}/{id}",
                new {controller="Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}