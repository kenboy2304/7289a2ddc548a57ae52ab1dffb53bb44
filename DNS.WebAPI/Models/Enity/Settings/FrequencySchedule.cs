using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNS.WebAPI.Models.Enity.Settings
{
    public class FrequencySchedule:General
    {

        [Display(Name = "Ngôn ngữ")]
        public string Language { get; set; }
        [Display(Name = "Mô tả")]
        public string Desc { get; set; }
    }
}