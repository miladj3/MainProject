using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.ViewModel.Admin.SlideShow;
using System.Data.Entity;
using DataLayer.Context;
using EntityFramework.Extensions;
using EFSecondLevelCache;

namespace ServiceLayer.EFServices
{
    public class SlideShowService : ISlideShowService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWorks;
        private readonly IDbSet<SlideShow> _slideShow;
        #endregion

        #region Contracture
        public SlideShowService(IUnitOfWork unitOfworks)
        {
            _unitOfWorks = unitOfworks;
            _slideShow = _unitOfWorks.Set<SlideShow>();
        }
        #endregion

        #region Method
        public void Add(AddSlideShowViewModel viewModel)
        {
            SlideShow model = new SlideShow
            {
                Link = viewModel.Link,
                ImageAltText = viewModel.ImageAltText,
                ImagePath = viewModel.ImagePath,
                Title = viewModel.Title,
                Description = viewModel.Description,
                DataHorizontal = viewModel.DataHorizontal,
                HideTransition = viewModel.HideTransition,
                Position = viewModel.Position,
                ShowTransition = viewModel.ShowTransition,
                DataVertical = viewModel.DataVertical
            };
            _slideShow.Add(model);
        }

        public async Task<Boolean> AllowAdd() =>
            await _slideShow.CountAsync() <= 5;

        public async Task Delete(Int64 id) =>
            await _slideShow.Where(x => x.Id == id).DeleteAsync();

        public async Task<EditSlideShowViewModel> GetByIdForEdit(Int64 id) =>
            await _slideShow.Where(x => x.Id == id).Select(x => new EditSlideShowViewModel()
            {
                Id = x.Id,
                ImageAltText = x.ImageAltText,
                ImagePath = x.ImagePath,
                Title = x.Title,
                Link = x.Link,
                Description = x.Description,
                DataHorizontal = x.DataHorizontal,
                HideTransition = x.HideTransition,
                Position = x.Position,
                ShowTransition = x.ShowTransition,
                DataVertical = x.DataVertical
            }).FirstOrDefaultAsync();

        public async Task<IEnumerable<SlideShow>> List() =>
            await _slideShow.OrderByDescending(x => x.Id).ToListAsync();

        public IEnumerable<SlideShow> ListForIndexPage() =>
             _slideShow.OrderByDescending(x => x.Id).Cacheable().ToList();

        public void Update(EditSlideShowViewModel viewModel)
        {
            SlideShow model = new SlideShow
            {
                Link = viewModel.Link,
                ImageAltText = viewModel.ImageAltText,
                ImagePath = viewModel.ImagePath,
                Title = viewModel.Title,
                Description = viewModel.Description,
                DataHorizontal = viewModel.DataHorizontal,
                HideTransition = viewModel.HideTransition,
                Position = viewModel.Position,
                ShowTransition = viewModel.ShowTransition,
                DataVertical = viewModel.DataVertical,
                 Id=viewModel.Id
            };
            _unitOfWorks.MarkAsChanged(model);
        }
        #endregion
    }
}
