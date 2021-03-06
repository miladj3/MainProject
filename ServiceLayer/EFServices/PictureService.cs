﻿using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using System.Data.Entity;
using DataLayer.Context;
using EntityFramework.Extensions;

namespace ServiceLayer.EFServices
{
    public class PictureService : IPictureService
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Picture> _pictures;
        #endregion

        #region Constructor
        public PictureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pictures = _unitOfWork.Set<Picture>();
        }
        #endregion

        #region CRUD
        public void Add(Picture picture)
        {
            _pictures.Add(picture);
        }

        public async Task Delete(Int64 id)
        {
           await  _pictures.Where(a => a.Id.Equals(id)).DeleteAsync();
        }

        public IEnumerable<Picture> GetAll(Int32 page, Int32 count, out Int32 total, Int64 folderId)
        {
            IQueryable<Picture> pircuters =
               _pictures.Include(a => a.Folder)
                   .AsNoTracking()
                   .Where(a => a.FolderId.Equals(folderId))
                   .OrderByDescending(a => a.Id);
            var totalQuery = pircuters.FutureCount();
            var query = pircuters.Skip((page - 1) * count).Take(count).Future();
            total = totalQuery.Value;
            return query.ToList();
        }

        public async Task<Picture> GetById(Int64 id) =>
            await _pictures.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Picture>> GetpictureByFolderId(Int64 folderId) =>
            await _pictures.AsNoTracking()
                    .Include(a => a.Folder)
                    .Where(a => a.FolderId == folderId)
                    .OrderByDescending(a => a.Id)
                    .ToListAsync();

        public async Task<String[]> GetPicturesOfFolder(Int64 id) =>
                await  _pictures.AsNoTracking()
                    .Include(a => a.Folder)
                    .Where(a => a.FolderId == id)
                    .Select(a => a.Path)
                    .ToArrayAsync();
        #endregion
    }
}
