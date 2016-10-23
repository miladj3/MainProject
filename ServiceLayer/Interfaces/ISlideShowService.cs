using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.SlideShow;

namespace ServiceLayer.Interfaces
{
    public interface ISlideShowService
    {
        void Add(AddSlideShowViewModel viewModel);
        void Update(EditSlideShowViewModel viewModel);
        Task Delete(Int64 id);
        Task<IEnumerable<SlideShow>> List();
        IEnumerable<SlideShow> ListForIndexPage();
        Task<EditSlideShowViewModel> GetByIdForEdit(Int64 id);
        Task<Boolean> AllowAdd();
    }
}
