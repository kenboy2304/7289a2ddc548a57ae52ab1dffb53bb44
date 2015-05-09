using System.Xml;

namespace DNS.WebAPI.Models.Model
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9", IsNullable = false, ElementName = "urlset")]
    public class SiteMapXml
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("url")]
        public SiteMapXmlItem[] Url { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class SiteMapXmlItem
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("loc")]
        public string Loc { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("lastmod")]
        public string Lastmod { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("changefreq")]
        public string Changefreq { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("priority")]
        public decimal Priority { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PrioritySpecified { get; set; }
    }
}