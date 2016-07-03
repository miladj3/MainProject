

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Entities
{
    [ComplexType]
    public class Rate
    {
        public virtual Double? TotalRating { get; set; }
        public virtual Int32? TotalRaters { get; set; }
        public virtual Double? AverageRating { get; set; }
    }
}