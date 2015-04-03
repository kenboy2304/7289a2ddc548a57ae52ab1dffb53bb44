using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models.Enity;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    public class ContentTypeManagerController : Controller
    {
        private DNSContext db = new DNSContext();

        // GET: DNSAdmin/ContentTypeManager
        public ActionResult Index()
        {
            return View(db.ContentTypes.ToList());
        }

        // GET: DNSAdmin/ContentTypeManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentType contentType = db.ContentTypes.Find(id);
            if (contentType == null)
            {
                return HttpNotFound();
            }
            return View(contentType);
        }

        // GET: DNSAdmin/ContentTypeManager/Create
        public ActionResult Create()
        {
            ViewBag.TypeInputs = db.TypeInputs.ToList();
            return View(new ContentType());
        }

        // POST: DNSAdmin/ContentTypeManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContentType contentType)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('ContentsOptionInputses', RESEED, 0)");
                contentType.ContentsOptionInputs = contentType.ContentsOptionInputs.Where(coi => !string.IsNullOrWhiteSpace(coi.Label)).ToList();
                db.ContentTypes.Add(contentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeInputs = db.TypeInputs.ToList();
            return View(contentType);
        }

        // GET: DNSAdmin/ContentTypeManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentType contentType = db.ContentTypes.Find(id);
            if (contentType == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeInputs = db.TypeInputs.ToList();
            return View(contentType);
        }

        // POST: DNSAdmin/ContentTypeManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContentType contentType)
        {
            
            if (ModelState.IsValid)
            {
                var update = db.ContentTypes.Find(contentType.Id);
                if (TryUpdateModel(update,new []{"Id","Name","DislayCatalog"}))
                {
                    update.ContentsOptionInputs.RemoveAll(coi => true);
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('dbo.ContentsOptionInputs', RESEED, 0)");
                    update.ContentsOptionInputs.AddRange(contentType.ContentsOptionInputs.Where(coi => !string.IsNullOrWhiteSpace(coi.Label)));
                    db.Entry(update).State = EntityState.Modified;
                    db.SaveChanges(); 
                }
                
                return RedirectToAction("Index");
            }
            return View(contentType);
        }

        // GET: DNSAdmin/ContentTypeManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentType contentType = db.ContentTypes.Find(id);
            if (contentType == null)
            {
                return HttpNotFound();
            }
            return View(contentType);
        }

        // POST: DNSAdmin/ContentTypeManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContentType contentType = db.ContentTypes.Find(id);
            db.ContentTypes.Remove(contentType);
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
