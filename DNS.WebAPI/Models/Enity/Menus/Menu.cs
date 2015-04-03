using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNS.WebAPI.Models.Enity.Menus
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public MenuType MenuType { get; set; }
    }

}