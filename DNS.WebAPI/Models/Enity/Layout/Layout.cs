using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNS.WebAPI.Models.Enity
{
    public class Layout
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}