using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using DNS.WebAPI.Models.Enity.Settings;
using PagedList;

namespace DNS.WebAPI.Controllers
{
    public class ModuleController : Controller
    {
        private DNSContext db = new DNSContext();
        // HOME: MODULE

        public ActionResult ArticleNewest(int size =10)
        {
            var articles =
                db.Articles.Where(a => a.Type == ArticleType.Article && a.Publish)
                    .OrderByDescending(a => a.PublishedDate)
                    .ToPagedList(1, size);
            return PartialView("Module/ArticleNewest", articles);
        }

        public ActionResult ConnectSocial()
        {

            return null;
        }

        public ActionResult PlayerModule(string date)
        {
            var d = DateTime.Now;
            var setting = db.Settings.Single(s => s.Name.Equals(typeof (GeneralSettings).Name));
            var general = setting.GetValue(new GeneralSettings());
            var playlist = new List<Article>
            {
                new Article
                {
                    Name = general.Intro.Name,
                    Mp3Url = general.Intro.DefaultMp3
                }
            };

            var articles = db.Articles.Where(a => a.Publish && 
                                                  a.PublishedDate.Day == d.Day &&
                                                  a.PublishedDate.Month == d.Month &&
                                                  a.PublishedDate.Year == d.Year).Include(a=>a.Catalogs).ToList();
            playlist.AddRange(articles);
            //Get Endding
            playlist.Add(
                new Article
                {
                    Name = general.Endding.Name,
                    Mp3Url = general.Endding.DefaultMp3
                }
            );

            return PartialView("Module/Player", playlist);
        }
        public ActionResult CategoryListModule()
        {
            var setting = db.Settings.Single(s => s.Name.Equals(typeof(BroadcastCatalog).Name));
            var lc = setting.GetValue(new BroadcastCatalog());
            ViewBag.Catalogs = db.Catalogs.Where(c => c.Publish).ToList();
            return PartialView("Module/Category", lc);
        }
        public ActionResult BroadcastScheduleModule()
        {
            var setting = db.Settings.Where(s => s.Name.Equals(typeof(BroadcastSchedule).Name)).ToList();
            var bs = setting.Select(s =>
            {
                var b = s.GetValue(new BroadcastSchedule());
                b.Id = s.Id;
                return b;
            });
            ViewBag.Catalogs = db.Catalogs.Where(c=>c.Publish).ToList();
            return PartialView("Module/BroadcastSchedule", bs);
        }
        public ActionResult BibleScheduleModule()
        {
            var setting = db.Settings.SingleOrDefault(s => s.Name.Equals(typeof (StudyBibleMonthly).Name) && s.Group.Equals(DateTime.Today.Month.ToString()));
            if (setting == null) return PartialView("Module/BibleSchedule", new StudyBibleMonthly());
            var monthly = setting.GetValue(new StudyBibleMonthly());
            return PartialView("Module/BibleSchedule", monthly);
        }

        public ActionResult FrequencyScheduleModule()
        {
            var setting = db.Settings.Where(s => s.Name.Equals(typeof (FrequencySchedule).Name)).ToList();
            var fs = setting.Select(s =>
            {
                var b = s.GetValue(new FrequencySchedule());
                b.Id = s.Id;
                return b;
            }).Where(f=>f.Publish);
            return PartialView("Module/FrequencySchedule", fs);
        }

        public ActionResult MainMenuModule()
        {
            var catalogs = db.Catalogs.Where(c => c.CatalogParentId == null &&c.ShowInMainMenu && c.Publish);
            return PartialView("Module/MainMenu",catalogs.ToList());
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