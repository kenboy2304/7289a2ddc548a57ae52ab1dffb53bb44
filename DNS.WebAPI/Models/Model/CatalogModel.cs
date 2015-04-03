using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace DNS.WebAPI.Models
{
    public class CatalogModel
    {
        public Catalog Catalog { get; set; }
        public IPagedList<Article> Articles { get; set; }
    }
}