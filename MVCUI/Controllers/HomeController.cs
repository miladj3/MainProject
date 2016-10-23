using DataLayer.Context;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    [T4MVC]
    public partial class HomeController : Controller
    {
        #region Fields
        private const int _1day = 86400;
        private const int _12hour = 43200;
        private const int _1houe = 3600;
        private const int _15min = 900;
        private const int _10min = 600;
        private readonly ISlideShowService _slideShow;
        private readonly IPageService _page;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISettingService _setting;
        #endregion

        #region Constructor
        public HomeController(IUnitOfWork unitOfWork,
                                            IPageService pageService,
                                            ISlideShowService slideShowService,
                                            ISettingService settingService)
        {
            _unitOfWork = unitOfWork;
            _page = pageService;
            _slideShow = slideShowService;
            _setting = settingService;
        }
        #endregion

        #region Actions
        [HttpGet]
        [OutputCache(CacheProfile ="long")]
        public virtual ActionResult Index()
        {
            ViewBag.Title = "خانه";
            return View(MVC.Home.Views.Index);
        }

        [ChildActionOnly]
        [HttpGet]
        [OutputCache(Duration =1, VaryByParam ="*")]
        public virtual PartialViewResult _slider() =>
            PartialView(MVC.Home.Views._Slider,_slideShow.ListForIndexPage());
        #endregion
    }
}