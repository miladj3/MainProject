using DataLayer.Context;
using DomainClasses.Enums;
using MVCUI.Filters;
using MVCUI.Helpers;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.ContactUs;

namespace MVCUI.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Contact")]
    [Route("{action}")]
    [T4MVC]
    [SiteAuthorize(Roles = "admin")]
    public partial class ContactController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactService _contact;
        #endregion

        #region Constracture
        public ContactController(IContactService contact, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._contact = contact;
        }
        #endregion

        #region Index & List
        [Route("Index")]
        [HttpGet]
        [OutputCache(CacheProfile = "nostore")]
        public virtual ActionResult Index() =>
          View(MVC.Admin.Contact.Views.Index);

        [OutputCache(Duration = 1, VaryByParam = "*")]
        public virtual PartialViewResult _list(Boolean isSeen = false, ContactType type = ContactType.All, Int32 page = 1, Int32 count = 10)
        {
            Int32 total;
            IEnumerable<ContactUsViewModel> model = _contact.GetDataTable(out total, page, count, type, isSeen);
            ViewBag.type = DropDown.GetTypeContactUsInAdmin(type);
            ViewBag.PageNumber = page;
            ViewBag.PageCount = count;
            ViewBag.TotalProducts = total;
            return PartialView(MVC.Admin.Contact.Views._list, model);
        }
        #endregion

        #region Show
        [HttpGet]
        [OutputCache(CacheProfile = "long")]
        [Route("ShowMessage/{id}")]
        public virtual async Task<ActionResult> Show(Int64? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            ContactUsViewModel model = await _contact.GetByIdForShow(id.Value);
            if (model == null)
                return HttpNotFound();
            model.IsSeen = true;
            await _unitOfWork.SaveAllChangesAsync();
            return View(MVC.Admin.Contact.Views.Show, model);
        }
        #endregion

        #region Delete 
        [HttpPost]
        [AjaxOnly]
        [Route("Delete")]
        [OutputCache(CacheProfile ="nostore")]
        public virtual async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                await _contact.Delete(id);
                await _unitOfWork.SaveAllChangesAsync();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
        #endregion
    }
}