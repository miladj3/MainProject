using DataLayer.Context;
using DomainClasses.Entities;
using EFSecondLevelCache;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Setting;

namespace ServiceLayer.EFServices
{
   public  class SettingService : ISettingService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWorks;
        private readonly IDbSet<SiteOption> _siteOption;
        #endregion

        #region Constracture
        public SettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWorks = unitOfWork;
             _siteOption = _unitOfWorks.Set<SiteOption>();
        }
        #endregion

        #region Methods
        public Boolean CommentMangement()
        {
            throw new NotImplementedException();
        }

        public EditSettingViewModel GetOptionsForEdit()
        {
            IList<SiteOption> opts = _siteOption.Cacheable().ToList();
            EditSettingViewModel _e = new EditSettingViewModel
            {
                StoreName = opts.SingleOrDefault(x => x.Name.Equals("StorName")).Value,
                StoreKeyWords = opts.SingleOrDefault(x => x.Name.Equals("StoreKeyWords")).Value,
                StoreDescription = opts.SingleOrDefault(x => x.Name.Equals("StoreDescription")).Value,
                Tel1 = opts.SingleOrDefault(x => x.Name.Equals("Tel1")).Value,
                Tel2 = opts.SingleOrDefault(x => x.Name.Equals("Tel2")).Value,
                PhoneNumber1 = opts.SingleOrDefault(x => x.Name.Equals("PhoneNumber1")).Value,
                PhoneNumber2 = opts.SingleOrDefault(x => x.Name.Equals("PhoneNumber2")).Value,
                CommentModeratorStatus = Convert.ToBoolean(opts.SingleOrDefault(x => x.Name.Equals("CommentModeratorStatus")).Value),
                Address = opts.SingleOrDefault(x => x.Name.Equals("Address")).Value
            };
            return _e;
        }

        public EditSettingViewModel GetOptionsForShowOnFooter()
        {
            IList<SiteOption> opts = _siteOption.Cacheable().ToList();
            EditSettingViewModel _e = new EditSettingViewModel
            {
                StoreName = opts.SingleOrDefault(x => x.Name.Equals("StorName")).Value,
                StoreKeyWords = opts.SingleOrDefault(x => x.Name.Equals("StoreKeyWords")).Value,
                StoreDescription = opts.SingleOrDefault(x => x.Name.Equals("StoreDescription")).Value,
                Tel1 = opts.SingleOrDefault(x => x.Name.Equals("Tel1")).Value,
                Tel2 = opts.SingleOrDefault(x => x.Name.Equals("Tel2")).Value,
                PhoneNumber1 = opts.SingleOrDefault(x => x.Name.Equals("PhoneNumber1")).Value,
                PhoneNumber2 = opts.SingleOrDefault(x => x.Name.Equals("PhoneNumber2")).Value,
                CommentModeratorStatus = Convert.ToBoolean(opts.SingleOrDefault(x => x.Name.Equals("CommentModeratorStatus")).Value),
                Address = opts.SingleOrDefault(x => x.Name.Equals("Address")).Value
            };
            return _e;
        }

        public void Update(EditSettingViewModel viewModel)
        {
            IList<SiteOption> opt = _siteOption.ToList();
            opt.SingleOrDefault(x => x.Name.Equals("StoreName")).Value = viewModel.StoreName;
            opt.SingleOrDefault(x => x.Name.Equals("StoreKeyWords")).Value = viewModel.StoreKeyWords;
            opt.SingleOrDefault(x => x.Name.Equals("StoreDescription")).Value = viewModel.StoreDescription;
            opt.SingleOrDefault(x => x.Name.Equals("Tel1")).Value = viewModel.Tel1;
            opt.SingleOrDefault(x => x.Name.Equals("Tel2")).Value = viewModel.Tel2;
            opt.SingleOrDefault(x => x.Name.Equals("PhoneNumber1")).Value = viewModel.PhoneNumber1;
            opt.SingleOrDefault(x => x.Name.Equals("PhoneNumber2")).Value = viewModel.PhoneNumber2;
            opt.SingleOrDefault(x => x.Name.Equals("CommentModeratorStatus")).Value = Convert.ToString(viewModel.CommentModeratorStatus);
            opt.SingleOrDefault(x => x.Name.Equals("Address")).Value = viewModel.Address;
        }
        #endregion
    }
}
