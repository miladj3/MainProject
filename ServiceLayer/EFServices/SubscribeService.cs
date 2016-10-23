using DataLayer.Context;
using DomainClasses.Entities;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Subscribe;

namespace ServiceLayer.EFServices
{
    public class SubscribeService : ISubscribeService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Subscribes> _subscribe;
        #endregion

        #region Constracture
        public SubscribeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._subscribe = _unitOfWork.Set<Subscribes>();
        }
        #endregion

        #region CRUD

        public async Task<Boolean> Add(SubscribeViewModel model)
        {
            Boolean result = await CheckEmailExist(model.EmailSubscribe);
            if (result)
                return false;

            _subscribe.Add(new Subscribes
            {
                Email = model.EmailSubscribe,
                Name = model.NameSubscribe
            });
            return true;
        }

        public async Task Update(SubscribeViewModel model)
        {
            Subscribes _model = await GetById(model.Id);
            _model.Name = model.NameSubscribe;
            _model.Email = model.EmailSubscribe;
        }

        public async Task Delete(Int64 id) =>
            await _subscribe.Where(x => x.Id == id).DeleteAsync();

        public async Task<Subscribes> GetById(Int64 id) =>
            await _subscribe.SingleOrDefaultAsync(x => x.Id == id);


        public async Task<IList<SubscribeViewModel>> List() =>
            await _subscribe.Select(x => new SubscribeViewModel
            {
                EmailSubscribe = x.Email,
                Id = x.Id,
                NameSubscribe = x.Name
            }).ToListAsync();

        public async Task<Boolean> CheckEmailExist(String email) =>
            await _subscribe.AnyAsync(x => x.Email == email) ? 
            true : 
            false;
        #endregion
    }
}
