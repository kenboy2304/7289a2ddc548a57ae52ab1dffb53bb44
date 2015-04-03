using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using DNS.WebAPI.Areas.DNSAdmin.Models;
using DNS.WebAPI.Models;

namespace DNS.WebAPI.Areas.DNSAdmin.Controllers
{
    public class CatalogResolve
    {
        public static IEnumerable<SelectListCatalogModels> GetSelectListAllLevelCatalog(IEnumerable<Catalog> root, int level = 0)
        {
            var data = new List<SelectListCatalogModels>();
            foreach (var catalog in root)
            {
                data.Add(new SelectListCatalogModels
                {
                    Id = catalog.Id,
                    Name = catalog.Name,
                    Level = level,
                    Disable = !string.IsNullOrWhiteSpace(catalog.Url)
                });

                if (catalog.CatalogChildrens != null && catalog.CatalogChildrens.Any())
                {
                    data.AddRange(GetSelectListAllLevelCatalog(catalog.CatalogChildrens.Where(c=>string.IsNullOrEmpty(c.Url)), level + 1));
                }
            }
            return data.AsEnumerable();
        }
    }
}