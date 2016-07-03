using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Contact
    {
        public virtual Int64 Id { get; set; }
        public virtual String Title { get; set; }
        public virtual String Content { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Byte[] RowVersion { get; set; }
        public virtual Boolean IsSeen { get; set; }
        public virtual ContactType Type { get; set; }
    }
}
