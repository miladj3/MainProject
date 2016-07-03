using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Page
    {
        public virtual Int64 Id { get; set; }
        public virtual String Content { get; set; }
        public virtual String Title { get; set; }
        public virtual String ImagePath { get; set; }
        public virtual String Description { get; set; }
        public virtual String KeyWords { get; set; }
        public virtual Int32 DisplayOrder { get; set; }
        public virtual Byte[] RowVersion { get; set; }
        public virtual PageShowPlace PageShowPlace { get; set; }
    }
}
