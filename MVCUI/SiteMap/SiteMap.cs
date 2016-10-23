using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MVCUI.SiteMap
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class SiteMap
    {
        private readonly List<DomainClasses.Entities.SiteMapEntity> _location;
        public SiteMap()
        {
            _location = new List<DomainClasses.Entities.SiteMapEntity>();
        }

        [XmlElement("url")]
        public List<DomainClasses.Entities.SiteMapEntity> locations
        {
            get { return _location; }
            set
            {
                foreach (DomainClasses.Entities.SiteMapEntity location in value)
                    Add(location);
            }
        }

        public void Add(DomainClasses.Entities.SiteMapEntity location) =>
            _location.Add(location);
    }
}