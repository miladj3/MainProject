using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DomainClasses.Enums;
using ViewModel.ViewModel.Page;
using System.Data.Entity;
using DataLayer.Context;
using EntityFramework.Extensions;
using EFSecondLevelCache;

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
        public Boolean Add(AddPageViewModel viewModel)
        {
            if (ExistByTitle(viewModel.Title))
                return false;
            Page page = new Page
            {
                Content = viewModel.Content,
                Title = viewModel.Title,
                Description = viewModel.Description,
                DisplayOrder = viewModel.DisplayOrder,
                ImagePath = viewModel.ImagePath,
                KeyWords = viewModel.KeyWords,
                PageShowPlace = viewModel.PageShowPlace
            };
            _pages.Add(page);
            return true;
        }

        public void Delete(Int64 id)
        {
            _pages.Where(x => x.Id.Equals(id)).Delete();
        }

        public Boolean Edit(EditPageViewModel viewModel)
        {
            if (ExistByTitle(viewModel.Title, viewModel.Id))
                return false;
            Page page = GetById(viewModel.Id);
            page.ImagePath = viewModel.ImagePath;
            page.KeyWords = viewModel.KeyWords;
            page.PageShowPlace = viewModel.PageShowPlace;
            page.Title = viewModel.Title;
            page.Content = viewModel.Content;
            page.Description = viewModel.Description;
            page.DisplayOrder = viewModel.DisplayOrder;
            return true;
        }

        public Page GetById(Int64 id) =>
            _pages.Find(id);

        public IEnumerable<Page> GetByShowPlace(PageShowPlace showPlace) =>
            _pages.AsNoTracking()
            .Where(x => x.PageShowPlace.Equals(showPlace))
            .OrderBy(x => x.DisplayOrder)
            .Cacheable().ToList();

        public IEnumerable<PageViewModel> GetDataTable(String term)
        {
            IQueryable<Page> pages = _pages.AsQueryable();
            if (!String.IsNullOrEmpty(term))
                pages = pages.Where(x => x.Title.Contains(term)).AsQueryable();
            return pages.Select(x => new PageViewModel()
            {
                Content = x.Content,
                Title = x.Title,
                LinkImage = x.ImagePath,
                ShowPlace = x.PageShowPlace == PageShowPlace.Body ? "داخل بدنه" : "فوتر سایت"
            }).ToList();
        }

        public EditPageViewModel GetForEditById(Int64 id) =>
            _pages.Where(x => x.Id.Equals(id)).Select(x => new EditPageViewModel()
            {
                Id = x.Id,
                ImagePath = x.ImagePath,
                Content = x.Content.Substring(0, 100),
                DisplayOrder = x.DisplayOrder,
                Description = x.Description,
                KeyWords = x.KeyWords,
                Title = x.Title,
                PageShowPlace = x.PageShowPlace
            }).FirstOrDefault();

        public Boolean ExistByTitle(String title) =>
            _pages.Any(x => x.Title.Equals(title));

        public Boolean ExistByTitle(String title, Int64 id) =>
            _pages.Any(x => x.Id != id && x.Title.Equals(title));
        #endregion
    }
}
