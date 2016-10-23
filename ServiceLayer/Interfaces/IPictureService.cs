using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IPictureService
    {
        void Add(Picture picture);
        Task Delete(Int64 id);
        Task<Picture> GetById(Int64 id);
        IEnumerable<Picture> GetAll(Int32 page, Int32 count, out Int32 total, Int64 folderId);
        Task<string[]> GetPicturesOfFolder(Int64 id);
        Task<IEnumerable<Picture>> GetpictureByFolderId(Int64 folderId);
    }
}
