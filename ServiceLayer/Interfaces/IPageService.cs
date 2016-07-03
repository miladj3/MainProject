using DomainClasses.Entities;
using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Page;

namespace ServiceLayer.Interfaces
{
    public interface IPageService
    {
        Boolean ExistByTitle(String title);
        Boolean ExistByTitle(String title, Int64 id);
        Boolean Add(AddPageViewModel viewModel);
        Boolean Edit(EditPageViewModel viewModel);
        void Delete(Int64 id);
        Page GetById(Int64 id);
        EditPageViewModel GetForEditById(Int64 id);
        IEnumerable<Page> GetByShowPlace(PageShowPlace showPlace);
        IEnumerable<PageViewModel> GetDataTable(String term);
    }
}
