using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Entities
{
    public class Value
    {
        public virtual Int64 Id { get; set; }
        public virtual String Content { get; set; }
        public virtual Attribute Attribute { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey("Product")]
        public virtual Int64 ProductId { get; set; }
        [ForeignKey("Attribute")]
        public virtual Int64 AttributeId { get; set; }
    }
}