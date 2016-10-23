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

        public async Task<Boolean> Add(AddFolderViewModel viewModel)
        {
            if (await CheckNameExist(viewModel.Name))
                return false;
            Folder model = new Folder()
            {
                Name = viewModel.Name
            };
            _folderService.Add(model);
            return true;
        }

        public async Task<Boolean> CheckNameExist(String name) =>
            await _folderService.AnyAsync(x => x.Name.Equals(name));

        public async Task<Boolean> CheckNameExist(String name, Int64 id) =>
            await _folderService.AnyAsync(x => x.Id != id && x.Name == name);

        public async Task Delete(Int64 id)
        {
           await _folderService.Where(x => x.Id == id).DeleteAsync();
        }

        public async Task<Boolean> Edit(EditFolderViewModel viewModel)
        {
            if (await CheckNameExist(viewModel.Name, viewModel.Id))
                return false;
            Folder folder = new Folder
            {
                Name = viewModel.Name,
                Id = viewModel.Id
            };
            _uniOfWork.MarkAsChanged<Folder>(folder);
            return true;
        }

        public async Task<EditFolderViewModel> GetForEdit(Int64 id) =>
            await _folderService.Where(x => x.Id.Equals(id)).Select(x => new EditFolderViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefaultAsync();

        public async Task<IEnumerable<Folder>> GetList() =>
            await _folderService.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        #endregion
    }
}
