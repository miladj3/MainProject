using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.ViewModel.Admin.Setting;
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

        public Boolean AllowAdd() =>
            _slideShow.Count() <= 10;

        public void Delete(Int64 id)
        {
            _slideShow.Where(x => x.Id.Equals(id)).Delete();
        }

        public EditSlideShowViewModel GetByIdForEdit(Int64 id) =>
            _slideShow.Where(x => x.Id.Equals(id)).Select(x => new EditSlideShowViewModel()
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
            }).FirstOrDefault();

        public IEnumerable<SlideShow> List() =>
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
                DataVertical = viewModel.DataVertical
            };
            _unitOfWorks.MarkAsChanged(model);
        }
        #endregion
    }
}
