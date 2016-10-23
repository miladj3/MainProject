using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.ContactUs;
using DomainClasses.Entities;
using DomainClasses.Enums;

namespace ServiceLayer.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactUsViewModel>> GetAll();
        Task<IEnumerable<ContactUsViewModel>> GetByType(ContactType type);
        Task<Contact> GetById(Int64 id);
        Task<ContactUsViewModel> GetByIdForShow(Int64 id);
        void Create(ContactUsViewModel model);
        Task Delete(Int64 id);
        Task DeleteByType(ContactType type);
        IEnumerable<ContactUsViewModel> GetDataTable(out Int32 total, Int32 page, Int32 count, ContactType type, Boolean isSeen);
    }
}
