using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DNS.WebAPI.Models;

namespace DNS.WebAPI.Areas.DNSAdmin.Models
{
    public class ArticleModel
    {
        public Article Article { get; set; }
        public IEnumerable<Catalog> AllCatalogs  { get; set; }
        private List<int> _selectedCatalogs;
        public List<int> SelectedCatalogs
        {
            get { return _selectedCatalogs ?? (_selectedCatalogs = Article.Catalogs.Select(m => m.Id).ToList()); }
            set { _selectedCatalogs = value; }
        }
    }
}