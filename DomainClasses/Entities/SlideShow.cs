using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class SlideShow
    {
        public virtual Int64 Id { get; set; }
        public virtual String Title { get; set; }
        public virtual String Description { get; set; }
        public virtual String ImagePath { get; set; }
        public virtual String ImageAltText { get; set; }
        public virtual String Link { get; set; }
        public virtual String Position { get; set; }
        public virtual String ShowTransition { get; set; }
        public virtual String HideTransition { get; set; }
        public virtual Int32 DataVertical { get; set; }
        public virtual Int32 DataHorizontal { get; set; }
        
    }
}
