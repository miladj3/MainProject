using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DataLayer.Context;
using System.Data.Entity;
using EntityFramework.Extensions;

namespace ServiceLayer.EFServices
{
    public class CommentService : ICommentService
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Comment> _comments;
        #endregion

        #region Constructure
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _comments = _unitOfWork.Set<Comment>();
        }
        #endregion

        #region Methods

        public void AddCommnet(Comment comment)
        {
            _comments.Add(comment);
        }

        public IEnumerable<Comment> GetProductComments(Int64 productId) =>
                                                        _comments.Include(x => x.Author)
                                                        .Include(x => x.Children)
                                                        .Where(x => x.Id == productId).ToList();

        public void Remove(Int64 id)
        {
            _comments.Where(x => x.Id == id).Delete();
        }

        public async Task RemoveUserComments(Int64 userId)
        {
            await _comments.Where(x => x.Author.id == userId).DeleteAsync();
        }
        #endregion

        #region Private Methods
        private void RecursiveRemove(Comment comment)
        {
            if (comment == null)
                return;
            if (!comment.Children.Any())
                return;
            foreach (Comment item in _comments)
            {
                RecursiveRemove(item);
                _comments.Where(x => x.Id == item.Id).Delete();
            }
        }
        #endregion
    }
}
