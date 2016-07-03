using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Entities
{
    public class Picture
    {
        public virtual Int64 Id { get; set; }
        public virtual String Path { get; set; }
        public virtual Folder Folder { get; set; }
        [ForeignKey("Folder")]
        public virtual Int64 FolderId { get; set; }
    }
}