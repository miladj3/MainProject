using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ICommentService
    {
        void Remove(Int64 id);
        void AddCommnet(Comment comment);
        IEnumerable<Comment> GetProductComments(Int64 productId);
        Task RemoveUserComments(Int64 userId);
    }
}
