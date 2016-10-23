using DomainClasses.Entities;
using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Page;
using ViewModel.ViewModel.Admin.SiteMap;

namespace ServiceLayer.Interfaces
{
    public interface IPageService
    {
        Task<Boolean> ExistByTitle(String title);
        Task<Boolean> ExistByTitle(String title, Int64 id);
        Task<Boolean> Add(AddPageViewModel viewModel);
        Task<Boolean> Edit(EditPageViewModel viewModel);
        Task Delete(Int64 id);
        Task<Page> GetById(Int64 id);
        Task<EditPageViewModel> GetForEditById(Int64 id);
        IEnumerable<Page> GetByShowPlace(PageShowPlace showPlace);
        IEnumerable<PageViewModel> GetDataTable(out Int32 total, Int32 page, Int32 count, String term);
        Task<IEnumerable<PageViewModel>> GetDataTable(String term);
        IEnumerable<PageViewModel> GetForShowFooter(Int32 take, PageOrderBy order);
        Task<IEnumerable<SiteMapShowViewModel>> GetAllForSiteMap();
    }
}
