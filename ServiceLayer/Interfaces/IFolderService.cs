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
        Boolean Add(AddFolderViewModel viewModel);
        void Delete(Int64 id);
        Boolean Edit(EditFolderViewModel viewModel);
        EditFolderViewModel GetForEdit(Int64 id);
        IEnumerable<Folder> GetList();
        Boolean CheckNameExist(String name);
        Boolean CheckNameExist(String name, Int64 id);
    }
}
