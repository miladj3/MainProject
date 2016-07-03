using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Setting;

namespace ServiceLayer.Interfaces
{
    public interface ISlideShowService
    {
        void Add(AddSlideShowViewModel viewModel);
        void Update(EditSlideShowViewModel viewModel);
        void Delete(Int64 id);
        IEnumerable<SlideShow> List();
        EditSlideShowViewModel GetByIdForEdit(Int64 id);
        bool AllowAdd();
    }
}
