using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.ViewModel.Admin.Folder;
using DataLayer.Context;
using System.Data.Entity;
using EntityFramework.Extensions;

namespace ServiceLayer.EFServices
{
    public class FolderService : IFolderService
    {

        #region Fields
        private readonly IUnitOfWork _uniOfWork;
        private readonly IDbSet<Folder> _folderService;
        #endregion

        #region Constructure
        public FolderService(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
            _folderService = _uniOfWork.Set<Folder>();
        }
        #endregion

        #region Methods

        public Boolean Add(AddFolderViewModel viewModel)
        {
            if (CheckNameExist(viewModel.Name))
                return false;
            Folder model = new Folder()
            {
                Name = viewModel.Name
            };
            _folderService.Add(model);
            return true;
        }

        public Boolean CheckNameExist(String name) =>
            _folderService.Any(x => x.Name.Equals(name));

        public Boolean CheckNameExist(String name, Int64 id) =>
            _folderService.Any(x => x.Id != id && x.Name == name);

        public void Delete(Int64 id)
        {
            _folderService.Where(x => x.Id == id).Delete();
        }

        public Boolean Edit(EditFolderViewModel viewModel)
        {
            if (!CheckNameExist(viewModel.Name))
                return false;
            Folder folder = new Folder
            {
                Name = viewModel.Name,
                Id = viewModel.Id
            };
            _uniOfWork.MarkAsChanged<Folder>(folder);
            return true;
        }

        public EditFolderViewModel GetForEdit(Int64 id) =>
            _folderService.Where(x => x.Id.Equals(id)).Select(x => new EditFolderViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault();

        public IEnumerable<Folder> GetList() =>
            _folderService.AsNoTracking().OrderBy(x => x.Id).ToList();
        #endregion
    }
}
