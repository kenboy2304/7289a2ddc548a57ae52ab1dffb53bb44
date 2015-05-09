using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models.Enity;

// ReSharper disable once CheckNamespace
namespace DNS.WebAPI.Models
{

    public class Catalog:General
    {
        [Display(Name = "Tên Danh Mục")]
        public string Name { get; set; }
        [Display(Name = "Url KeyWord")]
        public string UrlKeyWord { get; set; }
        [Display(Name = "Hình Đại Diện")]
        public string ThumbnailUrl { get; set; }
        [Display(Name = "Mô tả ngắn")]
        [DataType(DataType.MultilineText)]
        public string Brief { get; set; }
        [Display(Name = "Mô tả")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Desc { get; set; }
        [Display(Name = "Hiển thị trên Menu")]
        public bool ShowInMainMenu { get; set; }
        [Display(Name = "Download")]
        public bool AcceptDownload { get; set; }
        [Display(Name = "Link")]
        public string Url { get; set; }
        public virtual List<Article> Articles { get; set; }

        [ForeignKey("CatalogParent"), Display(Name = "Danh mục cha")]
        public int? CatalogParentId { get; set; }
        public virtual Catalog CatalogParent { get; set; }
        [Display(Name = "Danh mục con")]
        public virtual List<Catalog> CatalogChildrens { get; set; }
    }
}