using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DNS.WebAPI.Areas.DNSAdmin.Models;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using Newtonsoft.Json;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    public class ArticleBackup
    {
        public string Name { get; set; }
        public string Brief { get; set; }
        public string Content { get; set; }
        public int Id { get; set; }
        public string Mp3Url { get; set; }
        public bool Mp3Error { get; set; }

        public string Title { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string UrlKeyword { get; set; }
        public int Type { get; set; }
        public int Order { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Publish { get; set; }
        public int? ArticleParentId { get; set; }

        public string ThumbnailUrl { get; set; }
        public string Catalogs { get; set; }


    }
    public class DatabaseController : Controller
    {
        // GET: DNSAdmin/Database
        public ActionResult GetBackup()
        {
            using (var db = new DNSContext())
            {
                var data = db.Articles.ToList().Aggregate("", (current, article) =>
                {
                    var s = new JavaScriptSerializer().Serialize(new ArticleBackup
                    {
                        Name = article.Name,
                        Brief = article.Brief,
                        Content = article.Content,
                        Id = article.Id,

                        Mp3Url = article.Mp3Url,
                        Mp3Error = article.Mp3Error,

                        Title = article.Title,
                        MetaKeyword = article.MetaKeyword,
                        MetaDescription = article.MetaDescription,
                        UrlKeyword = article.UrlKeyword,
                        Type = (int)article.Type,

                        Order = article.Order,
                        Publish = article.Publish,
                        PublishDate = article.PublishedDate,
                        DateCreated = article.DateCreated,
                        ArticleParentId = article.ArticleParentId,
                        ThumbnailUrl = article.ThumbailUrl,
                        Catalogs =
                            article.Catalogs.Aggregate("",
                                (c, catalog) => c + (string.IsNullOrWhiteSpace(c) ? catalog.Name : (";" + catalog.Name)))
                    });

                    current = string.IsNullOrWhiteSpace(current) ? s : (current + Environment.NewLine + s);
                    return current;
                });

                var byteArray = Encoding.UTF8.GetBytes(data);
                var stream = new MemoryStream(byteArray);

                return File(stream, "text/plain","backup-"+DateTime.Now.ToString("dd-MM-yyy-hh-mm-ss-tt") + ".txt");
            }
        }
    }
}