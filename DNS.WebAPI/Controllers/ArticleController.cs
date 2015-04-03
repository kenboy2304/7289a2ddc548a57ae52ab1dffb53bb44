using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using DNS.WebAPI.Models.Enity.Settings;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace DNS.WebAPI.Controllers
{
    public class ArticleController : BaseController
    {
        DNSContext db = new DNSContext();
        // GET: Article
        public ActionResult Index(string name)
        {
            var article = db.Articles.SingleOrDefault(a => a.UrlKeyword.Equals(name));
            if (article == default(Article))
                return HttpNotFound();
            var catalog = article.Catalogs.FirstOrDefault();
            var releatedList = catalog.Articles.Where(a => a.Publish&& a.PublishedDate >=article.PublishedDate).OrderBy(a => a.PublishedDate).Take(3).OrderByDescending(a=>a.PublishedDate).ToList();
            releatedList.AddRange(catalog.Articles.Where(a => a.Publish&& a.PublishedDate <article.PublishedDate).OrderByDescending(a => a.PublishedDate).Take(10).ToList());
                
            var data = new ArticleDetailModel
            {
                Article = article,
                Catalog = catalog,
                ReleatedArticles = releatedList
                
            };
            return View(data);
        }
        public ActionResult Daily(string date)
        {
            var dateTime = DateTime.ParseExact(date, "d-M-yyyy", CultureInfo.InvariantCulture).Date;
            

            var setting = db.Settings.Single(s => s.Name.Equals(typeof(GeneralSettings).Name));
            var general = setting.GetValue(new GeneralSettings());
            var playlist = new List<Article>
            {
                new Article
                {
                    Name = general.Intro.Name,
                    Mp3Url = general.Intro.DefaultMp3
                }
            };
            var articles = db.Articles.Where(a => a.Publish && a.PublishedDate == dateTime.Date).ToList();
            playlist.AddRange(articles);
            playlist.Add(
                new Article
                {
                    Name = general.Endding.Name,
                    Mp3Url = general.Endding.DefaultMp3
                }
            );


            ViewBag.Date = dateTime;
            return View(playlist);
        }

        public ActionResult BibleDaily(int month)
        {
            var setting = db.Settings.SingleOrDefault(s => s.Name.Equals(typeof(StudyBibleMonthly).Name) && s.Group.Equals(month.ToString()));
            if (setting == default(Setting)) return HttpNotFound();
            var monthly = setting.GetValue(new StudyBibleMonthly());
            return View(monthly);
        }

        public ActionResult Page(string name)
        {
            var page = db.Articles.SingleOrDefault(a => a.UrlKeyword.Equals(name));
            return View(page);
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