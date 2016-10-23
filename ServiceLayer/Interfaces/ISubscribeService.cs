using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Subscribe;

namespace ServiceLayer.Interfaces
{
    public interface ISubscribeService
    {
        Task<Boolean> Add(SubscribeViewModel model);
        Task Update(SubscribeViewModel model);
        Task Delete(Int64 id);
        Task<Subscribes> GetById(Int64 id);
        Task<IList<SubscribeViewModel>> List();
        Task<Boolean> CheckEmailExist(String name);
    }
}
