using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNS.WebAPI.Areas.DNSAdmin.Models
{
    public class SelectListCatalogModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool Disable { get; set; }
    }
}