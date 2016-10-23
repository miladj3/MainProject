using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DomainClasses.Enums;
using ViewModel.ViewModel.Admin.Page;
using System.Data.Entity;
using DataLayer.Context;
using EntityFramework.Extensions;
using EFSecondLevelCache;
using EntityFramework.Future;
using ViewModel.ViewModel.Admin.SiteMap;
using System.Text.RegularExpressions;

namespace ServiceLayer.EFServices
{
    public class PageService : IPageService
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Page> _pages;
        #endregion

        #region Constructor
        public PageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pages = _unitOfWork.Set<Page>();
        }
        #endregion

        #region Methods
        public async Task<Boolean> Add(AddPageViewModel viewModel)
        {
            if (await ExistByTitle(viewModel.Title))
                return false;
            Page page = new Page
            {
                Content = viewModel.Content,
                Title = viewModel.Title,
                Description = viewModel.Description,
                DisplayOrder = viewModel.DisplayOrder,
                ImagePath = viewModel.ImagePath,
                KeyWords = viewModel.KeyWords,
                PageShowPlace = viewModel.PageShowPlace,
                DateCreated = DateTime.Now
            };
            _pages.Add(page);
            return true;
        }

        public async Task Delete(Int64 id) =>
            await _pages.Where(x => x.Id.Equals(id)).DeleteAsync();

        public async Task<Boolean> Edit(EditPageViewModel viewModel)
        {
            if (await ExistByTitle(viewModel.Title, viewModel.Id))
                return false;
            Page page = await GetById(viewModel.Id);
            page.ImagePath = viewModel.ImagePath;
            page.KeyWords = viewModel.KeyWords;
            page.PageShowPlace = viewModel.PageShowPlace;
            page.Title = viewModel.Title;
            page.Content = viewModel.Content;
            page.Description = viewModel.Description;
            page.DisplayOrder = viewModel.DisplayOrder;
            page.DateCreated = DateTime.Now;
            return true;
        }

        public async Task<Page> GetById(Int64 id) =>
            await _pages.SingleOrDefaultAsync(x => x.Id == id);

        public IEnumerable<Page> GetByShowPlace(PageShowPlace showPlace) =>
            _pages.AsNoTracking()
         .Where(x => x.PageShowPlace == showPlace)
         .OrderBy(x => x.DisplayOrder)
            .Cacheable().ToList();


        public async Task<IEnumerable<PageViewModel>> GetDataTable(String term)
        {
            IQueryable<Page> pages = _pages.AsNoTracking().AsQueryable();
            if (!String.IsNullOrEmpty(term))
                pages = pages.Where(x => x.Title.Contains(term)).AsQueryable();

            return await pages.Select(x => new PageViewModel()
            {
                Id = x.Id,
                Content = x.Content,
                Title = x.Title,
                LinkImage = x.ImagePath,
                ShowPlace = x.PageShowPlace == PageShowPlace.Body ? "داخل بدنه" : "فوتر سایت",
                DateCreated = x.DateCreated
            }).Cacheable().ToListAsync();
        }

        public IEnumerable<PageViewModel> GetDataTable(out Int32 total, Int32 page, Int32 count, String term)
        {
            IQueryable<Page> pages = _pages.AsNoTracking().OrderByDescending(x => x.DateCreated).AsQueryable();
            if (!String.IsNullOrEmpty(term))
                pages = pages.Where(x => x.Title.Contains(term)).OrderByDescending(x => x.DateCreated).AsQueryable();

            FutureCount totalQuery = pages.FutureCount();
            FutureQuery<PageViewModel> selectedQuery = pages.Skip((page - 1) * count).Take(count)
                .Select(x => new PageViewModel()
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    LinkImage = x.ImagePath,
                    ShowPlace = x.PageShowPlace == PageShowPlace.Body ? "داخل بدنه" : "فوتر سایت",
                    DateCreated = x.DateCreated
                }).Future();
            total = totalQuery.Value;
            return selectedQuery.ToList(); ;
        }

        public async Task<EditPageViewModel> GetForEditById(Int64 id) =>
            await _pages.Where(x => x.Id==id).Select(x => new EditPageViewModel()
            {
                Id = x.Id,
                ImagePath = x.ImagePath,
                Content = x.Content,
                DisplayOrder = x.DisplayOrder,
                Description = x.Description,
                KeyWords = x.KeyWords,
                Title = x.Title,
                PageShowPlace = x.PageShowPlace,
                DateCreated = x.DateCreated
            }).FirstOrDefaultAsync();

        public async Task<Boolean> ExistByTitle(String title) =>
           await _pages.AnyAsync(x => x.Title.Equals(title));

        public async Task<Boolean> ExistByTitle(String title, Int64 id) =>
            await _pages.AnyAsync(x => x.Id != id && x.Title.Equals(title));

        public IEnumerable<PageViewModel> GetForShowFooter(Int32 take, PageOrderBy order)
        {
            Regex regex = new Regex("\\<[^\\>]*\\>");

            IQueryable<Page> selectQuery = _pages.AsNoTracking().AsQueryable();
            switch (order)
            {
                case PageOrderBy.Title:
                    selectQuery = selectQuery.OrderBy(x => x.Title).AsQueryable();
                    break;
                case PageOrderBy.Date:
                    selectQuery = selectQuery.OrderByDescending(x => x.DateCreated).AsQueryable();
                    break;
            }
            IEnumerable<PageViewModel> model = selectQuery.Take(take).Select(x => new PageViewModel
            {
                Content = x.Content,
                DateCreated = x.DateCreated,
                Id = x.Id,
                LinkImage = x.ImagePath,
                Title = x.Title
            }).Cacheable().ToList();

            foreach (var item in model)
                item.Content = regex.Replace(item.Content, String.Empty);

            return model;
        }

        public async Task<IEnumerable<SiteMapShowViewModel>> GetAllForSiteMap() =>
            await _pages.AsNoTracking().Select(x => new SiteMapShowViewModel
            {
                Id = x.Id,
                Name = x.Title
            }).Distinct().ToListAsync();

        #endregion
    }
}
