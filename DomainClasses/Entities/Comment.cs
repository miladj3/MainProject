using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Comment
    {
        public virtual Int64 Id { get; set; }
        public virtual String Content { get; set; }
        public virtual Boolean IsApproved { get; set; }
        public virtual Byte[] RowVersion { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<User> LikedUser { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual Int64 ProductId { get; set; }
        public virtual Rate Rate { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual Int64? ParentId { get; set; }
        public virtual ICollection<Comment> Children { get; set; }
        public virtual CommentSectionType SectionType { get; set; }
    }
}
