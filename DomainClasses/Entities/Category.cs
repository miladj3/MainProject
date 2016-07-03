using System;
using System.Collections.Generic;

namespace DomainClasses.Entities
{
    public class Category
    {
        public virtual Int64 Id { get; set; }
        public virtual String Name { get; set; }
        public virtual Category Parent { get; set; }
        public virtual Int64? ParentId { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual Int32 DisplayOrder { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual String Description { get; set; }
        public virtual String KeyWords { get; set; }
        public virtual Int32 DiscountPercent { get; set; }
        public virtual Byte[] RowVersion { get; set; }
    }
}