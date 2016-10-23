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
    public class SettingService : ISettingService
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

        public async Task<EditSettingViewModel> GetOptionsForEdit()
        {
            IList<SiteOption> opts = await _siteOption.Cacheable().ToListAsync();
            String storeName = opts.FirstOrDefault(x => x.Name == "StoreName").Value;
            String storeKeyWord = opts.FirstOrDefault(x => x.Name == "StoreKeyWords").Value;
            String storedescription = opts.FirstOrDefault(x => x.Name == "StoreDescription").Value;
            String tel1 = opts.FirstOrDefault(x => x.Name == "Tel1").Value;
            String tel2 = opts.FirstOrDefault(x => x.Name == "Tel2").Value;
            String phoneNumber1 = opts.FirstOrDefault(x => x.Name == "PhoneNumber1").Value;
            String phoneNumber2 = opts.FirstOrDefault(x => x.Name == "PhoneNumber2").Value;
            String email = opts.FirstOrDefault(x => x.Name == "Email").Value;
            Boolean trust = Convert.ToBoolean(opts.FirstOrDefault(x => x.Name == "Trust").Value);
            Boolean commentModeratstore = Convert.ToBoolean(opts.FirstOrDefault(x => x.Name == "CommentModeratorStatus").Value);
            String address = opts.FirstOrDefault(x => x.Name == "Address").Value;
            String logo = opts.FirstOrDefault(x => x.Name == "Logo").Value;
            String Telegram = opts.FirstOrDefault(x => x.Name == "Telegram").Value;
            String Instagram = opts.FirstOrDefault(x => x.Name == "Instagram").Value;
            String Facebook = opts.FirstOrDefault(x => x.Name == "Facebook").Value;
            EditSettingViewModel _e = new EditSettingViewModel
            {
                StoreName = storeName,
                StoreKeyWords = storeKeyWord,
                StoreDescription = storedescription,
                Tel1 = tel1,
                Tel2 = tel2,
                PhoneNumber1 = phoneNumber1,
                PhoneNumber2 = phoneNumber2,
                CommentModeratorStatus = commentModeratstore,
                Address = address,
                Email = email,
                Trust = trust,
                Logo = logo,
                Facebook = Facebook,
                Instagram = Instagram,
                Telegram = Telegram
            };
            return _e;
        }

        public EditSettingViewModel GetOptionsForShowOnFooter()
        {
            IList<SiteOption> opts = _siteOption.Cacheable().ToList();
            EditSettingViewModel _e = new EditSettingViewModel
            {
                StoreName = opts.FirstOrDefault(x => x.Name == "StoreName").Value,
                StoreKeyWords = opts.FirstOrDefault(x => x.Name == "StoreKeyWords").Value,
                StoreDescription = opts.FirstOrDefault(x => x.Name == "StoreDescription").Value,
                Tel1 = opts.FirstOrDefault(x => x.Name == "Tel1").Value,
                Tel2 = opts.FirstOrDefault(x => x.Name == "Tel2").Value,
                PhoneNumber1 = opts.FirstOrDefault(x => x.Name == "PhoneNumber1").Value,
                PhoneNumber2 = opts.FirstOrDefault(x => x.Name == "PhoneNumber2").Value,
                Email = opts.FirstOrDefault(x => x.Name == "Email").Value,
                Trust = Convert.ToBoolean(opts.FirstOrDefault(x => x.Name == "Trust").Value),
                CommentModeratorStatus = Convert.ToBoolean(opts.FirstOrDefault(x => x.Name == "CommentModeratorStatus").Value),
                Address = opts.FirstOrDefault(x => x.Name == "Address").Value,
                Logo = opts.FirstOrDefault(x => x.Name == "Logo").Value,
                Telegram = opts.FirstOrDefault(x => x.Name == "Telegram").Value,
                Facebook = opts.FirstOrDefault(x => x.Name == "Facebook").Value,
                Instagram = opts.FirstOrDefault(x => x.Name == "Instagram").Value,
            };
            return _e;
        }

        public async Task Update(EditSettingViewModel viewModel)
        {
            IList<SiteOption> opt = await _siteOption.ToListAsync();
            opt.FirstOrDefault(x => x.Name.Equals("StoreName")).Value = viewModel.StoreName;
            opt.FirstOrDefault(x => x.Name.Equals("StoreKeyWords")).Value = viewModel.StoreKeyWords;
            opt.FirstOrDefault(x => x.Name.Equals("StoreDescription")).Value = viewModel.StoreDescription;
            opt.FirstOrDefault(x => x.Name.Equals("Tel1")).Value = viewModel.Tel1;
            opt.FirstOrDefault(x => x.Name.Equals("Tel2")).Value = viewModel.Tel2;
            opt.FirstOrDefault(x => x.Name.Equals("PhoneNumber1")).Value = viewModel.PhoneNumber1;
            opt.FirstOrDefault(x => x.Name.Equals("PhoneNumber2")).Value = viewModel.PhoneNumber2;
            opt.FirstOrDefault(x => x.Name.Equals("Email")).Value = viewModel.Email;
            opt.FirstOrDefault(x => x.Name.Equals("Trust")).Value = Convert.ToString(viewModel.Trust);
            opt.FirstOrDefault(x => x.Name.Equals("CommentModeratorStatus")).Value = Convert.ToString(viewModel.CommentModeratorStatus);
            opt.FirstOrDefault(x => x.Name.Equals("Address")).Value = viewModel.Address;
            opt.FirstOrDefault(x => x.Name == "Logo").Value = viewModel.Logo;
            opt.FirstOrDefault(x => x.Name == "Telegram").Value = viewModel.Telegram;
            opt.FirstOrDefault(x => x.Name == "Instagram").Value = viewModel.Instagram;
            opt.FirstOrDefault(x => x.Name == "Facebook").Value = viewModel.Facebook;
        }
        #endregion
    }
}
