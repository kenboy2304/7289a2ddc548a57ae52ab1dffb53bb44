using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace DNS.WebAPI.Controllers
{
    public class CatalogController : BaseController
    {
        DNSContext db = new DNSContext();
        // GET: Catalog
        public ActionResult Index(string name, int page=1,int  size =10)
        {
            if (string.IsNullOrWhiteSpace(name)) return HttpNotFound();
            var catalogModel = new CatalogModel
            {
                Catalog = db.Catalogs.SingleOrDefault(c => c.UrlKeyWord.Equals(name)),
                Articles =
                    db.Articles.Where(a => a.Catalogs.Any(c => c.UrlKeyWord.Equals(name))&&a.Publish)
                        .OrderByDescending(a => a.PublishedDate)
                        .ToPagedList(page, size)
            };
            if (catalogModel.Catalog == default(Catalog))
                return HttpNotFound();
            
            return View(catalogModel);
        }
        //public ActionResult Index(int id)
        //{
        //    if (id == 0) return HttpNotFound();

        //    return View();
        //}
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