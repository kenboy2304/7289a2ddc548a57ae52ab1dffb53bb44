using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using Microsoft.Owin.Security.Provider;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    [Authorize(Roles = "Admin,Manager,Post")]
    public class CatalogManagerController : Controller
    {
        private DNSContext db = new DNSContext();

        // GET: DNSAdmin/CatalogManager
        public ActionResult Index()
        {
            var catalogs = db.Catalogs.Where(c=>c.CatalogParentId==null);
            return View(catalogs.ToList());
        }

        // GET: DNSAdmin/CatalogManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // GET: DNSAdmin/CatalogManager/Create
        public ActionResult Create()
        {
            ViewBag.CatalogParentId = new SelectList(CatalogResolve.GetSelectListAllLevelCatalog(db.Catalogs.Where(c => c.CatalogParentId == null).ToList()), "Id", "Name");
            return View();
        }


        

            // POST: DNSAdmin/CatalogManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UrlKeyWord,ThumbnailUrl,Brief,Desc,ShowInMainMenu,Url,CatalogParentId,Publish,Order")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                catalog.DateCreated = DateTime.Now;
                db.Catalogs.Add(catalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatalogParentId = new SelectList(CatalogResolve.GetSelectListAllLevelCatalog(db.Catalogs.Where(c => c.CatalogParentId == null).ToList()), "Id", "Name");
            return View(catalog);
        }

        // GET: DNSAdmin/CatalogManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            var selectlist = CatalogResolve.GetSelectListAllLevelCatalog(db.Catalogs.Where(c => c.CatalogParentId == null).ToList()).Select(o => new
            {
                o.Id,
                Name = GetName(o.Name,o.Level)
            });
            ViewBag.CatalogParentId = new SelectList(selectlist, "Id", "Name",catalog.CatalogParentId.HasValue?catalog.CatalogParentId.Value: 0);
            return View(catalog);
        }

        private string GetName(string name, int level)
        {
            var prefix = "";
            for (var i = 0; i < level; i++)
            {
                prefix += "-----";
            }
            return prefix+name;
        }

        // POST: DNSAdmin/CatalogManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Name,UrlKeyWord,ThumbnailUrl,Brief,Desc,ShowInMainMenu,Url,CatalogParentId,Publish,Order")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                var update = db.Catalogs.Find(catalog.Id);
                if (TryUpdateModel(update, new[] { "Id", "Name", "UrlKeyWord", "ThumbnailUrl", "Brief", "Desc", "ShowInMainMenu", "Url", "CatalogParentId", "Publish", "Order" }))
                {
                    db.Entry(update).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CatalogParentId = new SelectList(CatalogResolve.GetSelectListAllLevelCatalog(db.Catalogs.Where(c => c.CatalogParentId == null).ToList()), "Id", "Name");
            return View(catalog);
        }

        // GET: DNSAdmin/CatalogManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // POST: DNSAdmin/CatalogManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catalog catalog = db.Catalogs.Find(id);
            db.Catalogs.Remove(catalog);
            db.SaveChanges();
            return RedirectToAction("Index");
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
