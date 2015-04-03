using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DNS.WebAPI.Models.Enity.Content;

namespace DNS.WebAPI.Models.Enity
{
    public class TypeInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        [DataType(DataType.Html),Display(Name = "Giao diện quản lý")]
        public string ManagerTemplate { get; set; }

        [AllowHtml]
        [DataType(DataType.Html), Display(Name = "Giao diện hiển thị")]
        public string DisplayTemplate { get; set; }

        public virtual List<ContentsOptionInputs> ContentsOptionInputses { get; set; }
    }
}