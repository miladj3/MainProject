using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;
using ViewModel.ViewModel.Admin.ContactUs;
using DataLayer.Context;
using DomainClasses.Entities;
using System.Data.Entity;
using EntityFramework.Extensions;
using EntityFramework.Future;

namespace ServiceLayer.EFServices
{
    public class ContactService : IContactService
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Contact> _contact;
        #endregion

        #region Constracture
        public ContactService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._contact = _unitOfWork.Set<Contact>();
        }
        #endregion

        public void Create(ContactUsViewModel ViewModel)
        {
            Contact model = new Contact()
            {
                Content = ViewModel.Content,
                Date = DateTime.Now,
                EmailOrPhone = ViewModel.EmailOrPhone,
                IsSeen = false,
                Name = ViewModel.Name,
                Title = ViewModel.Title,
                Type = ViewModel.Type
            };
            _contact.Add(model);
        }

        public async Task Delete(Int64 id) =>
           await _contact.Where(x => x.Id == id).DeleteAsync();

        public async Task DeleteByType(ContactType type) =>
            await _contact.Where(x => x.Type == type).DeleteAsync();

        public async Task<IEnumerable<ContactUsViewModel>> GetAll() =>
            await _contact.AsNoTracking().Select(x => new ContactUsViewModel
            {
                 Id=x.Id,
                Content = x.Content,
                Date = x.Date,
                EmailOrPhone = x.EmailOrPhone,
                IsSeen = x.IsSeen,
                Name = x.Name,
                Title = x.Title,
                Type = x.Type
            }).ToListAsync();

        public async Task<Contact> GetById(Int64 id) =>
            await _contact.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<ContactUsViewModel> GetByIdForShow(Int64 id)
        {
            Contact t = await _contact.SingleOrDefaultAsync(x => x.Id == id);
            t.IsSeen = true;
            return new ContactUsViewModel
            {
                Content = t.Content,
                Date = t.Date,
                EmailOrPhone = t.EmailOrPhone,
                IsSeen = t.IsSeen,
                Name = t.Name,
                Id = t.Id,
                Title = t.Title,
                Type = t.Type
            };
        }

        public async Task<IEnumerable<ContactUsViewModel>> GetByType(ContactType type) =>
              await _contact.AsNoTracking().Where(x => x.Type == type).Select(x => new ContactUsViewModel
              {
                   Id=x.Id,
                  Content = x.Content,
                  Date = x.Date,
                  EmailOrPhone = x.EmailOrPhone,
                  IsSeen = x.IsSeen,
                  Name = x.Name,
                  Title = x.Title,
                  Type = x.Type
              }).ToListAsync();

        public IEnumerable<ContactUsViewModel> GetDataTable(out Int32 total, Int32 page, Int32 count, ContactType type, Boolean isSeen)
        {
            IQueryable<Contact> model = _contact.AsNoTracking().OrderByDescending(x => x.Date).AsQueryable();
            if (isSeen)
                model = model.Where(x => x.IsSeen != isSeen);

            model = model.Skip((page - 1) * count).Take(count);
            FutureCount totalQuery = model.FutureCount();
            switch (type)
            {
                case ContactType.All:
                    break;
                case ContactType.Problem:
                case ContactType.Order:
                case ContactType.Offer:
                case ContactType.Criticism:
                    model = model.Where(x => x.Type == type);
                    break;
            }
            total = totalQuery.Value;
            return model.Select(x => new ContactUsViewModel
            {
                Id = x.Id,
                Content = x.Content,
                Date = x.Date,
                EmailOrPhone = x.EmailOrPhone,
                IsSeen = x.IsSeen,
                Name = x.Name,
                Title = x.Title,
                Type = x.Type
            }).ToList();
        }
    }
}
