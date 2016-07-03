using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Folder
    {
        public virtual Int64 Id { get; set; }
        public virtual String Name { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
