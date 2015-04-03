using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models.Enity;

// ReSharper disable once CheckNamespace
namespace DNS.WebAPI.Models
{
    public class Article:General
    {

        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Url Keyword")]
        public string UrlKeyword { get; set; }
        [Display(Name = "Đường dẫn hình")]
        public string ThumbailUrl { get; set; }
        [Display(Name = "Đường dẫn mp3")]
        public string Mp3Url { get; set; }
        public bool Mp3Error { get; set; }

        [Display(Name = "Mô tả ngăn"), DataType(DataType.MultilineText)]
        public string Brief { get; set; }
        [Display(Name = "Nội dung"), DataType(DataType.Html), AllowHtml]
        public string Content { get; set; }
        [Display(Name = "Ngày đăng")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - hh:mm tt}")]
        public DateTime PublishedDate { get; set; }
        [Display(Name = "Danh mục")]
        public virtual List<Catalog> Catalogs { get; set; }
        [Display(Name = "Playlist")]
        public virtual List<Article> ArticlePlayList { get; set; }

        [ForeignKey("ArticleParent"), Display(Name = "Playlist")]
        public int? ArticleParentId { get; set; }
        [Display(Name = "Playlist")]
        public virtual Article ArticleParent { get; set; }

        public ArticleType Type { get; set; }
    }

    public enum ArticleType
    {
        Page, Article, Playlist
    }
}