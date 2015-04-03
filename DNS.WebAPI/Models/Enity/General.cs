using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace DNS.WebAPI.Models.Enity
{
    public class General
    {
        public General()
        {
            DateCreated = DateTime.Now;
        }
        

        public int Id { get; set; }
        [Display(Name = "Hiển thị")]
        public bool Publish { get; set; }
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy - hh:mm tt}")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Sắp xếp")]
        public int Order { get; set; }

        //SEO
        [Display(Name = "Tiêu đề trang")]
        public string Title { get; set; }
        [Display(Name = "Từ khóa trên trang"), DataType(DataType.MultilineText)]
        public string MetaKeyword { get; set; }
        [Display(Name = "Mô tả trên trang"), DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }

    }
}