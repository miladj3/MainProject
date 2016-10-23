using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Folder;

namespace ServiceLayer.Interfaces
{
    public interface IFolderService
    {
        Task<Boolean> Add(AddFolderViewModel viewModel);
        Task Delete(Int64 id);
        Task<Boolean> Edit(EditFolderViewModel viewModel);
        Task<EditFolderViewModel> GetForEdit(Int64 id);
        Task<IEnumerable<Folder>> GetList();
        Task<Boolean> CheckNameExist(String name);
        Task<Boolean> CheckNameExist(String name, Int64 id);
    }
}
