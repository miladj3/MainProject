using DataLayer.Context;
using DomainClasses.Entities;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Page;

namespace MVCUI.Controllers
{
    [T4MVC]
    [RoutePrefix("Blog")]
    [Route("{action}")]
    public partial class BlogController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPageService _page;
        #endregion

        #region Constractur
        public BlogController(IUnitOfWork unitOfWork, IPageService page)
        {
            this._unitOfWork = unitOfWork;
            this._page = page;
        }
        #endregion

        #region Actions
        [HttpGet]
        [Route("Index/{page?}")]
        [OutputCache(CacheProfile = "long")]
        public virtual ActionResult Index(Int32? page)
        {
            Int32 _pageNumber = page ?? 1,
                pageSize = 7,
                total;
            IEnumerable<PageViewModel> model = _page.GetDataTable(out total, _pageNumber, pageSize, null);
            ViewBag.page = _pageNumber;
            ViewBag.count = total;
            ViewBag.pageSize = pageSize;
            ViewBag.Title = "وبلاگ";
            return View(MVC.Blog.Views.Index, model);
        }

        [HttpGet]
        [Route("{id}/{title}")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> blog(Int64 id, String title)
        {
            Page model = await _page.GetById(id);
            PageViewModel _p = new PageViewModel
            {
                Content = model.Content,
                Description = model.Description,
                KeyWords = model.KeyWords,
                LinkImage = model.ImagePath,
                Title = model.Title,
                Id = model.Id,
                DateCreated = model.DateCreated
            };
            _p.Content = _p.Content.Replace("<div>", String.Empty);
            _p.Content = _p.Content.Replace("</div>", String.Empty);
            return View(MVC.Blog.Views.blog, _p);
        }
        #endregion
    }
}