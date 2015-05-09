using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DNS.WebAPI.Models;
using DNS.WebAPI.Models.Enity;
using DNS.WebAPI.Models.Model;

namespace DNS.WebAPI.Controllers
{
    public class SystemController : ApiController
    {
        private DNSContext db = new DNSContext();
        private string host = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
        [Route("category-sitemap.xml")]
        [HttpGet]
        public SiteMapXml CatalogSiteMap()
        {
            //HttpContext.Current.Request.Url.Authority
            var catalogs = GetCatalog();
            var sitemapItems = catalogs.Select(c => new SiteMapXmlItem
            {
                Loc = string.IsNullOrWhiteSpace(c.Url) ? host + "/" + c.UrlKeyWord : c.Url.StartsWith("http")? c.Url : host + c.Url ,
                Priority = 1,
                Changefreq = "daily",
            });
            return new SiteMapXml {Url = sitemapItems.ToArray()};
        }
        [Route("post-sitemap.xml")]
        [HttpGet]
        public SiteMapXml ArticleSiteMap()
        {
            var articles = db.Articles.Where(a=>a.Type == ArticleType.Article && a.Publish && a.PublishedDate<= DateTime.Today).OrderByDescending(a=>a.PublishedDate).ToList();
            var sitemapItems = articles.Select(a => new SiteMapXmlItem
            {
                Loc = host + "/"+a.Catalogs.First().UrlKeyWord+"/"+a.UrlKeyword+".dns" , 
                Priority = 1,
                Changefreq = "daily",
            });

            return new SiteMapXml { Url = sitemapItems.ToArray() };
        }

        private IEnumerable<Catalog> GetCatalog(int parentId=0)
        {
            var list = new List<Catalog>();
            var catalogs = 
                parentId == 0 
                ? db.Catalogs.Where(c => c.CatalogParentId == null && c.Publish).ToList() 
                : db.Catalogs.Where(c => c.CatalogParentId == parentId && c.Publish).ToList();

            foreach (var catalog in catalogs)
            {
                if(catalog.Url!="#")
                    list.Add(catalog);
                if (catalog.CatalogChildrens.Any())
                {
                    list.AddRange(GetCatalog(catalog.Id));
                }
            }
            return list;
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
