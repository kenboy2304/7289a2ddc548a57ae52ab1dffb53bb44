using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    public class PageManagerController : Controller
    {
        private DNSContext db = new DNSContext();

        // GET: DNSAdmin/PageManager
        public ActionResult Index()
        {
            var articles = db.Articles.Where(a => a.Type == ArticleType.Page);
            return View(articles.ToList());
        }

        // GET: DNSAdmin/PageManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: DNSAdmin/PageManager/Create
        public ActionResult Create()
        {
            return View(new Article{DateCreated = DateTime.Now, PublishedDate = DateTime.Now});
        }

        // POST: DNSAdmin/PageManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.Type = ArticleType.Page;
                article.DateCreated = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return article.Publish ? RedirectToAction("Index") : RedirectToAction("Edit", new {id = article.Id});
            }

            ViewBag.ArticleParentId = new SelectList(db.Articles, "Id", "Name", article.ArticleParentId);
            return View(article);
        }

        // GET: DNSAdmin/PageManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleParentId = new SelectList(db.Articles, "Id", "Name", article.ArticleParentId);
            return View(article);
        }

        // POST: DNSAdmin/PageManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ThumbailUrl,Mp3Url,Brief,Content,Publish,Order")] Article article)
        {
            if (ModelState.IsValid)
            {
                var update = db.Articles.Find(article.Id);
                if (TryUpdateModel(update, new[] { "Id","Name","ThumbailUrl","Mp3Url","Brief","Content","Publish","Order" }))
                {
                    if (article.Publish) update.PublishedDate = DateTime.Now;
                    db.Entry(update).State = EntityState.Modified;
                    db.SaveChanges();
                    return update.Publish ? RedirectToAction("Index"): RedirectToAction("Edit", new {id = update.Id});
                }
            }
            ViewBag.ArticleParentId = new SelectList(db.Articles, "Id", "Name", article.ArticleParentId);
            return View(article);
        }

        // GET: DNSAdmin/PageManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: DNSAdmin/PageManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
