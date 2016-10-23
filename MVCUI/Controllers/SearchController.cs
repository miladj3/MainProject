using DataLayer.Context;
using DomainClasses.Enums;
using MVCUI.Helpers;
using MVCUI.Searching;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Product;

namespace MVCUI.Controllers
{
    [T4MVC]
    [RoutePrefix("search")]
    public partial class SearchController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _product;
        #endregion

        #region Constacture
        public SearchController(IUnitOfWork unitofwork, IProductService product)
        {
            this._unitOfWork = unitofwork;
            this._product = product;
        }
        #endregion

        #region Actions
        [HttpGet]
        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        [Route("Category/{categoryId}/page/{page=1}/count/{count=8}/filter/{filter=all}/keyword/{keyword?}")]
        public virtual ActionResult Index(Int64 categoryId, Int32 page, Int32 count, PSFilter filter, String keyword)
        {
            Int32 total;
            IEnumerable<ProductSectionViewModel> _data = _product.DataListSearch(out total, keyword.RemoveHtmlTags(), page, count, filter, categoryId);

            ViewBag.Counts = DropDown.GetSearchPageCount(count);
            ViewBag.Filters = DropDown.GetSearchFilters(filter);
            SearchViewModel model = new SearchViewModel
            {
                CategoryId = categoryId,
                Count = count,
                Filter = filter,
                Page = page,
                Term = keyword,
                Total = total,
                Products = _data
            };
            return View(MVC.Search.Views.Index, model);
        }
        #endregion

        #region Search & AutoComplete
        [OutputCache(CacheProfile = "nostore")]
        [HttpGet]
        public virtual ActionResult Search(String word)
        {
            ViewBag.word = word;

            if (String.IsNullOrEmpty(word))
            {
                if (Request.IsAjaxRequest())
                    return PartialView(MVC.Search.Views._AutoComplete, new List<ProductSearchResultViewModel>());
                else
                    return View(MVC.Search.Views.Search, new List<ProductSearchResultViewModel>());
            }
            IList<ProductSearchResultViewModel> result = LuceneProducts.GetTermsScored(word);
            
            if (Request.IsAjaxRequest())
                return PartialView(MVC.Search.Views._AutoComplete, result);

            return View(MVC.Search.Views.Search, result);
        }
        #endregion
    }
}