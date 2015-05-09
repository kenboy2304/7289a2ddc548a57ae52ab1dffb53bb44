using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StudyBibleDailyManagerController : Controller
    {
        private DNSContext db = new DNSContext();
        // GET: DNSAdmin/StudyBibleDailyManager
        public ActionResult Index()
        {
            return View();
        }
        // GET: DNSAdmin/StudyBibleDailyManager/Edit/5
        public ActionResult Edit(Month month)
        {
            var setting = db.Settings.SingleOrDefault(s => s.Name.Equals(typeof(StudyBibleMonthly).Name) && s.Group.Equals(((int)month).ToString()));
            var sbd = setting == default(Setting) ? new StudyBibleMonthly{Month = month} : setting.GetValue(new StudyBibleMonthly());
            if (setting != null) sbd.Id = setting.Id;
            return View(sbd);
        }

        // POST: DNSAdmin/StudyBibleDailyManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudyBibleMonthly monthly)
        {
            if (ModelState.IsValid)
            {
                if (monthly.Id == 0)
                {
                    var setting = new Setting
                    {
                        Name = typeof(StudyBibleMonthly).Name,
                        Group = ((int)monthly.Month).ToString()
                    };
                    setting.SetValue(monthly);
                    db.Settings.Add(setting);
                    db.SaveChanges();  
                }
                else
                {
                    var setting = db.Settings.Find(monthly.Id);
                    setting.SetValue(monthly);
                    db.Entry(setting).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.Update = "Cập nhật thành công!";
            }
            else
            {
                ViewBag.Error = "Cập nhật thất bại!";
            }
            return View(monthly);
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
