using DomainClasses.Enums;
using System;
using System.Xml.Serialization;

namespace DomainClasses.Entities
{
    public class SiteMapEntity
    {
        [XmlElement("loc")]
        public String Url { get; set; }

        [XmlElement("lastmod")]
        public DateTime? LastModified { get; set; }
        public Boolean ShouldSerializeLastModified() =>
            LastModified.HasValue;

        [XmlElement("changefreq")]
        public ChangeFreq? ChangeFrequency { get; set; }
        public Boolean ShouldSerializeChangeFrequency() =>
            ChangeFrequency.HasValue;

        [XmlElement("priority")]
        public float? priority { get; set; }
        public Boolean ShouldSerializePriority() =>
            priority.HasValue;
    }
}
