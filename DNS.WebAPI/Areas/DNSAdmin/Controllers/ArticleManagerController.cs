﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using PagedList;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    [Authorize(Roles = "Admin,Manager,Post")]
    public class ArticleManagerController : Controller
    {
        private DNSContext db = new DNSContext();

        // GET: DNSAdmin/ArticleManager
        public ActionResult Index(string name="",int page = 1, int size = 10)
        {
            
            var articles = string.IsNullOrWhiteSpace(name)
                                    ?db.Articles.Where(a => a.Type == ArticleType.Article).OrderByDescending(a=>a.PublishedDate).ToPagedList(page, size)
                                    : db.Articles.Where(a => a.Type == ArticleType.Article&& a.Title.Contains(name)).OrderByDescending(a=>a.PublishedDate).ToPagedList(page, size);
            return View(articles);
        }
        public ActionResult Error(int page = 1)
        {
            var list = db.Articles.ToList()
                .Where(article => 
                        db.Articles.Count(a => a.Name == article.Name) > 1)
            .OrderBy(a => a.Name).ThenByDescending(a => a.PublishedDate).ToPagedList(page, 100);
            return View("Index", list);
        }
        public ActionResult ErrorLink(int page = 1)
        {
            var list = db.Articles.Where(article => article.Mp3Error)
            .OrderBy(a => a.Name).ThenByDescending(a => a.PublishedDate).ToPagedList(page, 100);
            return View("Index", list);
        }

        // GET: DNSAdmin/ArticleManager/Details/5
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

        // GET: DNSAdmin/ArticleManager/Create
        public ActionResult Create()
        {
            ViewBag.Catalogs = CatalogResolve.GetSelectListAllLevelCatalog(db.Catalogs.Where(c => c.CatalogParentId == null).ToList());
            return View(new Article());
        }

        public ActionResult Import()
        {
            
            return View(new List<MediaModel>());
        }
        [HttpPost]
        public ActionResult Import(string json)
        {
            var arr = Regex.Split(json, "\r\n");
            var list = arr.Where(a=>!string.IsNullOrWhiteSpace(a)).Select(a =>new JavaScriptSerializer().Deserialize<MediaModel>(a.Substring(0, a.Length-1))).ToList();
            foreach (var obj in list)
            {
                Catalog catalog;
                if (!db.Catalogs.Any(c => c.Name.Equals(obj.CategoryId)))
                {
                    catalog = new Catalog
                    {
                        Name = obj.CategoryId,
                        UrlKeyWord = MediaModel.ConvertToUnsign3(obj.CategoryId),
                        Publish = true,
                        DateCreated = DateTime.Now.ToLocalTime(),
                        Title = obj.CategoryId,
                        CatalogParentId = 2,
                        ShowInMainMenu = true,
                        AcceptDownload = true

                    };
                    db.Catalogs.Add(catalog);
                    db.SaveChanges();
                }
                else
                {
                    catalog = db.Catalogs.Single(c => c.Name.Equals(obj.CategoryId));
                }
                var article = new Article
                {
                    Name = obj.Name,

                    DateCreated = obj.PulishDate.ToLocalTime(),
                    PublishedDate = obj.PulishDate.ToLocalTime(),
                    UrlKeyword = obj.GetUrlKeyword(),
                    Mp3Url = obj.MediaUrl,
                    Mp3Error = obj.IsError,
                    Catalogs = new List<Catalog> { catalog },
                    Type = ArticleType.Article,
                    Title = obj.Name,
                    Publish = true
                };
                if (db.Articles.Any(a => a.Name == article.Name && a.PublishedDate == article.PublishedDate))
                    continue;
                db.Articles.Add(article);
                db.SaveChanges();
            }
            return View(list);
        }



        // POST: DNSAdmin/ArticleManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Article article, int[] catalogIds)
        {
            if (catalogIds != null)
                article.Catalogs = db.Catalogs.Where(c => catalogIds.Any(id => id.Equals(c.Id))).ToList();
            if (ModelState.IsValid)
            {

                article.Type = ArticleType.Article;
                article.DateCreated = article.PublishedDate = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return article.Publish ? RedirectToAction("Index") : RedirectToAction("Edit", new { id = article.Id });
            }

            ViewBag.Catalogs = CatalogResolve.GetSelectListAllLevelCatalog(db.Catalogs.Where(c => c.CatalogParentId == null ).ToList());
            return View(article);
        }

        // GET: DNSAdmin/ArticleManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.Catalogs = CatalogResolve.GetSelectListAllLevelCatalog(db.Catalogs.Where(c => c.CatalogParentId == null ).ToList());
            return View(article);
        }

        // POST: DNSAdmin/ArticleManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article, int[] catalogIds)
        {
            if (ModelState.IsValid)
            {
                var update = db.Articles.Find(article.Id);
                if (TryUpdateModel(update,new[] {"Id", "Name", "ThumbailUrl", "Mp3Url", "Brief", "Content", "Publish", "Order"}))
                {
                    update.Catalogs.RemoveAll(c => true);
                    if (catalogIds != null)
                        update.Catalogs = db.Catalogs.Where(c => catalogIds.Any(id => id == c.Id)).ToList();
                    db.Entry(update).State = EntityState.Modified;
                    db.SaveChanges();

                    


                }

                return article.Publish ? RedirectToAction("Index") : RedirectToAction("Edit", new { id = article.Id });
            }
            ViewBag.ArticleParentId = new SelectList(db.Articles, "Id", "Name", article.ArticleParentId);
            return View(article);
        }

        // GET: DNSAdmin/ArticleManager/Delete/5
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

        // POST: DNSAdmin/ArticleManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            var article = db.Articles.Find(id);
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
