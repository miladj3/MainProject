using DataLayer.Context;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    public partial class HomeController : Controller
    {
        #region Fields
        private const int _1day = 86400;
        private const int _12hour = 43200;
        private const int _1houe = 3600;
        private const int _15min = 900;
        private const int _10min = 600;
        private readonly IProductService _product;
        private readonly ISlideShowService _slideShow;
        private readonly IPageService _page;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISettingService _setting;
        private readonly ICategoryService _category;
        #endregion

        #region Constructor
        public HomeController(IUnitOfWork unitOfWork,
                                            ICategoryService categoryService,
                                            IPageService pageService, 
                                            ISlideShowService slideShowService, 
                                            ISettingService settingService,
                                             IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _category = categoryService;
            _page = pageService;
            _slideShow = slideShowService;
            _product = productService;
            _setting = settingService;
        }
        #endregion

        #region section Index
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
        #endregion
    }
}