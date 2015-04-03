using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models.Enity;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    public class DashboardController : Controller
    {
        DNSContext db = new DNSContext();
        // GET: DNSAdmin/Dashboard
        public ActionResult Index()
        {
            var from = DateTime.Today.AddDays(-5);
            var to = DateTime.Today.AddDays(5);
            var articles = db.Articles.Where(a => a.PublishedDate >= from && a.PublishedDate <= to).OrderByDescending(a=>a.PublishedDate);
            
            return View(articles);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
