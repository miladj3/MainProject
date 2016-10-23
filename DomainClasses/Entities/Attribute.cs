using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Attribute
    {
        [Key]
        public virtual Int64 Id { get; set; }
        public virtual String Name { get; set; }
        public virtual  Category Category { get; set; }
        public virtual AttributeType Type { get; set; }
        public virtual Int64 CategoryId { get; set; }
        public virtual Byte[] RowVersion { get; set; }
        public virtual ICollection<Value> Values { get; set; }
    }
}
