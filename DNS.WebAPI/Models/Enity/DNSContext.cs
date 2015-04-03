using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DNS.WebAPI.Models.Enity.Content;

namespace DNS.WebAPI.Models.Enity
{
// ReSharper disable once InconsistentNaming
    public class DNSContext : DbContext
    {
        public DNSContext():base("DNSData"){}
        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Article> Articles { get; set; }

        public DbSet<Layout> Layouts { get; set; }

        public DbSet<TypeInput> TypeInputs { get; set; }

        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<ContentsOptionInputs> ContentsOptionInputses { get; set; }
          
    }
}