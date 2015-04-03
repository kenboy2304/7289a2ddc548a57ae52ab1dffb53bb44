using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using DNS.WebAPI.Models.Enity.Settings;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    public class SettingsController : Controller
    {

        private DNSContext db = new DNSContext();

        #region Broadcast Schedule

        public ActionResult General()
        {
            var name = typeof(GeneralSettings).Name;
            Setting setting;
            //in it
            if (!db.Settings.Any(s => s.Name.Equals(name)))
            {

                setting = new Setting { Name = typeof(GeneralSettings).Name };
                setting.SetValue(new GeneralSettings());
                db.Settings.Add(setting);
                db.SaveChanges();
            }
            setting = db.Settings.Single(s => s.Name.Equals(name));
            var gs = setting.GetValue(new GeneralSettings());
            gs.Id = setting.Id;
            return View(gs);
        }
        [HttpPost]
        public ActionResult General(GeneralSettings general)
        {
            var setting = db.Settings.Find(general.Id);
            setting.SetValue(general);
            db.Entry(setting).State = EntityState.Modified;
            db.SaveChanges();
            return View(general);
        }


        public ActionResult BroadcastSchedule()
        {
            var name = typeof (BroadcastSchedule).Name;

            #region Old
            //in it
            //if (!db.Settings.Any(s => s.Name.Equals(name)))
            //{
            //    foreach (DayBroadcast day in Enum.GetValues(typeof (DayBroadcast)))
            //    {
            //        var bs = new BroadcastSchedule {Day = day};
            //        var setting = new Setting {Name = typeof (BroadcastSchedule).Name};
            //        setting.SetValue(bs);
            //        db.Settings.Add(setting);
            //        db.SaveChanges();
            //    }
            //} 
            #endregion

            var bssetting = db.Settings.Where(s => s.Name.Equals(name)).ToList();
            var data = bssetting.Select(s =>
            {
                var b = s.GetValue(new BroadcastSchedule());
                b.Id = s.Id;
                return b;
            });

            ViewBag.Catalogs = db.Catalogs.Where(c => c.Publish).ToList();

            return View(data.ToList());
        }

        [HttpPost]
        public ActionResult BroadcastUpdate(List<BroadcastSchedule> broadcastSchedules)
        {
            var day = 0;
            foreach (var broadcastSchedule in broadcastSchedules)
            {
                broadcastSchedule.Day = (DayBroadcast)day;
                var setting = db.Settings.Find(broadcastSchedule.Id);
                setting.SetValue(broadcastSchedule);
                db.Entry(setting).State = EntityState.Modified;
                db.SaveChanges();
                day++;
            }
            return RedirectToAction("BroadcastSchedule");
        }

        #endregion

        public ActionResult BroadcastCatalogs()
        {
            var name = typeof(BroadcastCatalog).Name;

            var setting = db.Settings.Single(s => s.Name.Equals(name));
            var bc = setting.GetValue(new BroadcastCatalog());
            bc.Id = setting.Id;
            ViewBag.Catalogs = db.Catalogs.Where(c => c.Publish).ToList();
            return View(bc);
        }

        [HttpPost]
        public ActionResult BroadcastCatalogs([Bind(Include = "Id")] BroadcastCatalog broadcastCatalog, string[] cats)
        {
            var setting = db.Settings.Find(broadcastCatalog.Id);
            var b = setting.GetValue(new BroadcastCatalog());
            b.CatalogList = cats != null ? string.Join(",", cats) : "";
            setting.SetValue(b);
            db.Entry(setting).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Catalogs = db.Catalogs.Where(c => c.Publish).ToList();
            return RedirectToAction("BroadcastCatalogs");
        }


        #region Frequency Schedule
        public ActionResult FrequencySchedule()
        {
            var name = typeof(FrequencySchedule).Name;
            var fs = db.Settings.Where(s => s.Name.Equals(name)).ToList();
            var data = fs.Select(s =>
            {
                var b = s.GetValue(new FrequencySchedule());
                b.Id = s.Id;
                return b;
            });
            return View(data);
        }

        public ActionResult FrequencyCreate()
        {

            return View();
        }

        [HttpPost]
        public ActionResult FrequencyCreate([Bind(Include = "Id,Language,Desc,Publish")] FrequencySchedule frequency)
        {
            frequency.DateCreated = DateTime.Now;
            var setting = new Setting{Name = typeof(FrequencySchedule).Name};
            setting.SetValue(frequency);
            if (ModelState.IsValid)
            {
                db.Settings.Add(setting);
                db.SaveChanges();
                return RedirectToAction("FrequencySchedule");
            }
            return View(frequency);
        }

        public ActionResult FrequencyDetail(int id)
        {
            var setting = db.Settings.Find(id);
            var fs = setting.GetValue(new FrequencySchedule());
            fs.Id = setting.Id;
            return View(fs);
        }

        public ActionResult FrequencyEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            var fs = setting.GetValue(new FrequencySchedule());
            fs.Id = setting.Id;
            return View(fs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FrequencyEdit(FrequencySchedule frequency)
        {
            
            if (ModelState.IsValid)
            {
                var setting = db.Settings.Find(frequency.Id);
                setting.SetValue(frequency);
                db.Entry(setting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FrequencySchedule");
            }
            return View(frequency);
        }

        // GET: DNSAdmin/Menus/Delete/5
        public ActionResult FrequencyDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var setting = db.Settings.Find(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            var fs = setting.GetValue(new FrequencySchedule());
            fs.Id = setting.Id;
            return View(fs);
        }

        // POST: DNSAdmin/Menus/Delete/5
        [HttpPost, ActionName("FrequencyDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFrequencyConfirmed(int id)
        {
            var setting= db.Settings.Find(id);
            db.Settings.Remove(setting);
            db.SaveChanges();
            return RedirectToAction("FrequencySchedule");
        }
        #endregion


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
