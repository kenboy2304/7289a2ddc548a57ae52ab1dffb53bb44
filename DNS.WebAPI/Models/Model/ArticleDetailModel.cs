using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace DNS.WebAPI.Models
{
    public class ArticleDetailModel
    {
        public Article Article { get; set; }
        public Catalog Catalog { get; set; }
        public List<Article> ReleatedArticles { get; set; }
    }
}