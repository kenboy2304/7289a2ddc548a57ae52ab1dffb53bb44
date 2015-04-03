using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models.Enity;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    public class TypeInputManagerController : Controller
    {
        private DNSContext db = new DNSContext();

        // GET: DNSAdmin/TypeInputManager
        public ActionResult Index()
        {
            return View(db.TypeInputs.ToList());
        }

        // GET: DNSAdmin/TypeInputManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeInput typeInput = db.TypeInputs.Find(id);
            if (typeInput == null)
            {
                return HttpNotFound();
            }
            return View(typeInput);
        }

        // GET: DNSAdmin/TypeInputManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DNSAdmin/TypeInputManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ManagerTemplate,DisplayTemplate")] TypeInput typeInput)
        {
            if (ModelState.IsValid)
            {
                db.TypeInputs.Add(typeInput);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeInput);
        }

        // GET: DNSAdmin/TypeInputManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeInput typeInput = db.TypeInputs.Find(id);
            if (typeInput == null)
            {
                return HttpNotFound();
            }
            return View(typeInput);
        }

        // POST: DNSAdmin/TypeInputManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ManagerTemplate,DisplayTemplate")] TypeInput typeInput)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeInput).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeInput);
        }

        // GET: DNSAdmin/TypeInputManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeInput typeInput = db.TypeInputs.Find(id);
            if (typeInput == null)
            {
                return HttpNotFound();
            }
            return View(typeInput);
        }

        // POST: DNSAdmin/TypeInputManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeInput typeInput = db.TypeInputs.Find(id);
            db.TypeInputs.Remove(typeInput);
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
