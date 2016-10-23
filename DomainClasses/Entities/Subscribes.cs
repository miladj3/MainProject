using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Subscribes
    {
        public virtual Int64 Id { get; set; }
        [EmailAddress]
        public virtual String Email { get; set; }
        [DataType(DataType.Text)]
        public virtual String Name { get; set; }
    }
}
