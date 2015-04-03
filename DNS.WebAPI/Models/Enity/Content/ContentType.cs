using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DNS.WebAPI.Models.Enity.Content;

namespace DNS.WebAPI.Models.Enity
{
    public class ContentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Dislay In Catalogs")]
        public bool DislayCatalog { get; set; }
        [Display(Name = "Content Type Options")]
        public virtual List<ContentsOptionInputs> ContentsOptionInputs { get; set; }

    }
}