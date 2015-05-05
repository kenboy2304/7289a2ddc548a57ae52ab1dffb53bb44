using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNS.WebAPI.Models.Enity.Settings
{
    public class GeneralSettings
    {

        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Google API"), DataType(DataType.MultilineText)]
        public string GoogleApi { get; set; }


        public int Id { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Mô tả"), DataType(DataType.MultilineText)]
        public string Desc { get; set; }
        [Display(Name = "Từ khóa"), DataType(DataType.MultilineText)]
        public string KeyWord { get; set; }

        public string CopyRight { get; set; }
        
        
        public string Facebook { get; set; }
        public string Twitter { get; set; }

        [Display(Name = "Google+")]
        public string GooglePlus { get; set; }

        public string Youtube { get; set; }

        [Display(Name = "Mở đầu")]
        public DefaultMedia Intro { get; set; }

        [Display(Name = "Kết thúc")]
        public DefaultMedia Endding { get; set; }
    }

    public class DefaultMedia
    {
        [Display(Name="Tên")]
        public string Name { get; set; }

        [Display(Name = "Url")]
        public string DefaultMp3 { get; set; }
    }
}