using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Address
    {
        [Key]
        public virtual Int64 Id { get; set; }
        public virtual String CompanyName { get; set; }
        public virtual String State { get; set; }
        public virtual String City { get; set; }
        public virtual String AddressLine1 { get; set; }
        public virtual String AddressLine2 { get; set; }
        public virtual Byte[] RowVersion { get; set; }
        public virtual User User { get; set; }
    }
}
