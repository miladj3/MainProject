using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Role
    {
        public virtual Int64 Id { get; set; }
        public virtual String Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual String Description { get; set; }
    }
}
    