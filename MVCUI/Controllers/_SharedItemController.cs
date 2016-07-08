using DataLayer.Context;
using DomainClasses.Entities;
using ServiceLayer.EFServices;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    public partial class _SharedItemController : Controller
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _category;
        private const int _1day = 86400;
        private const int _12hour = 43200;
        private const int _1houe = 3600;
        private const int _15min = 900;
        private const int _10min = 600;
        #endregion

        #region Constracture
        public _SharedItemController(IUnitOfWork unitOfWork, ICategoryService category)
        {
            _unitOfWork = unitOfWork;
            _category = category;
        }
        #endregion

        #region Actions
        [HttpGet]
        public virtual PartialViewResult NavBar()
        {
            return PartialView(MVC._SharedItem.Views._navbar);
        }

        [HttpGet]
        public virtual PartialViewResult Menu()
        {
            return PartialView(MVC._SharedItem.Views._menu);
        }

        [HttpGet]
        public virtual PartialViewResult Search()
        {
            return PartialView(MVC._SharedItem.Views._search);
        }
        #endregion
    }
}