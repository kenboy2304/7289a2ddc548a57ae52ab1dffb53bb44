using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DNS.WebAPI.Models.Enity;
using DNS.WebAPI.Models.Enity.Settings;

namespace DNS.WebAPI.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            using (var db = new DNSContext())
            {
                var setting = db.Settings.Single(s => s.Name.Equals(typeof (GeneralSettings).Name));
                var general = setting.GetValue(new GeneralSettings());
                ViewBag.Name = general.Name;
                ViewBag.Address = general.Address;
                ViewBag.Phone = general.Phone;
                ViewBag.Email = general.Email;

                ViewBag.Title = general.Title;
                ViewBag.Desc = general.Desc;
                ViewBag.Keyword = general.KeyWord;
                ViewBag.CopyRight = general.CopyRight;
                
                ViewBag.Facebook = general.Facebook;
                ViewBag.Twitter = general.Twitter;
                ViewBag.GooglePlus = general.GooglePlus;
                ViewBag.Youtube = general.Youtube;
            }

            base.Initialize(requestContext);
        }
    }
}