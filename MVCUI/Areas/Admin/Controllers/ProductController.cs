using DataLayer.Context;
using DomainClasses.Entities;
using MVCUI.Extentions;
using MVCUI.Filters;
using MVCUI.Helpers;
using MVCUI.Searching;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Product;

namespace MVCUI.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Product")]
    [Route("{action}")]
    [SiteAuthorize]
    [T4MVC]
    public partial class ProductController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _product;
        private readonly IValueService _value;
        private readonly ICategoryService _category;
        private readonly IAttributeService _attribute;
        private readonly IProductImageService _productImage;
        #endregion

        #region Constracture
        public ProductController(IUnitOfWork unitOfWork,
                                                IValueService value,
                                                ICategoryService category,
                                                IProductService product,
                                                IAttributeService attribut,
                                                IProductImageService productImage)
        {
            this._unitOfWork = unitOfWork;
            this._product = product;
            this._value = value;
            this._category = category;
            this._attribute = attribut;
            this._productImage = productImage;
        }
        #endregion

        #region Index & List
        [HttpGet]
        [Route("Index")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> Index()
        {
            await PopulateAllCategoriesDropDownList(0);
            return View();
        }

        [OutputCache(VaryByParam = "*", Duration = 1)]
        public virtual PartialViewResult List(Boolean freeSend = false,
            Boolean delete = false,
            String term = "",
            Int32 page = 1,
            Int32 PageCount = 10,
            DomainClasses.Enums.Order Order = DomainClasses.Enums.Order.Descending,
            DomainClasses.Enums.ProductOrderBy ProductOrderBy = DomainClasses.Enums.ProductOrderBy.Name,
            Int64 categoryId = 0,
            DomainClasses.Enums.ProductType type = DomainClasses.Enums.ProductType.All)
        {
            int total;
            IEnumerable<ProductViewModel> product = _product.DataList(total: out total, term: term, deleted: delete, freeSend: freeSend, page: page, count: PageCount, categoryId: categoryId, productOrderBy: ProductOrderBy, order: Order, productType: type);
            ProductListViewModel model = new ProductListViewModel
            {
                CategoryId = categoryId,
                Deleted = delete,
                FreeSend = freeSend,
                Order = Order,
                PageCount = PageCount,
                PageNumber = page,
                ProductList = product,
                ProductOrderBy = ProductOrderBy,
                ProductType = type,
                Term = term,
                TotalProducts = total
            };

            ViewBag.ProductOrderByList = DropDown.GetProductOrderByList(ProductOrderBy);
            ViewBag.CountList = DropDown.GetCountList(PageCount);
            ViewBag.OrderList = DropDown.GetOrderList(Order);

            return PartialView(MVC.Admin.Product.Views._List, model);
        }
        #endregion

        #region Create
        [HttpGet]
        [Route("Add-Product")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> Create()
        {
            await PopulateAllCategoriesDropDownList(null);
            return View();
        }

        [HttpPost]
        [Route("Add-Product")]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAllCategoriesDropDownList(null);
                ModelState.AddModelError("", "اعتبارسنجی اطلاعات وارد شده معتبر نمیباشد");
                return View(MVC.Admin.Product.Views.Create, model);
            }
            Product product = new Product
            {
                Name = model.Name,
                ApplyCategoryDiscount = model.ApplyCategoryDiscount,
                CategoryId = model.CategoryId,
                Deleted = model.Deleted,
                IsFreeShipping = model.IsFreeShipping,
                Description = model.Description,
                DiscountPercent = model.DiscountPercent,
                MetaDescription = model.MetaDescription,
                MetaKeywords = model.MetaKeyWords,
                Price = model.Price,
                Ratio = model.Ratio,
                SellCount = 0,
                ViewCount = 0,
                Reserve = 0,
                Stock = model.Stock,
                Type = model.Type,
                Rate = new Rate() { AverageRating = 0, TotalRaters = 0, TotalRating = 0 },
                AddedDate = DateTime.Now,
                PrincipleImagePath = model.PrincipleImagePath,
                NotificationStockMinimum = model.NotificationStockMinimum,
                PriceAfterDiscount = model.Price.CalculateDiscountPrice(model.DiscountPercent),
                ComingSoon = model.ComingSoon,
                HomePage = model.HomePage,
                SpecialSell = model.SpecialSell
            };
            _product.Insert(product);
            IEnumerable<DomainClasses.Entities.Attribute> attributes = await _attribute.GetAttributesByCategoryId(model.CategoryId);
            _value.AddCategoryAttributesToProduct(attributes, product.Id);
            await _unitOfWork.SaveChangesAsync();

            LuceneProducts.AddIndex(new ProductLuceneViewModel()
            {
                Name = product.Name,
                Id = product.Id,
                ImagePath = product.PrincipleImagePath,
                Description = product.Description
            });
            CacheManager.InvalidateChildActionsCache();
            return RedirectToAction(MVC.Admin.Product.ActionNames.Create, MVC.Admin.Product.Name);
        }
        #endregion

        #region Edit
        [HttpGet]
        [Route("Edit/{id:long}")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> Edit(Int64? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditProductViewModel model = await _product.GetForEdit(id.Value);

            if (model == null)
                return HttpNotFound();

            await PopulateAllCategoriesDropDownList(id.Value);

            return View(MVC.Admin.Product.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا اطلاعات مورد نیاز را با دقت پر نمایید");
                await PopulateAllCategoriesDropDownList(model.CategoryId);
                return View(MVC.Admin.Product.Views.Edit, model);
            }
            await _product.Update(model);
            if (model.OldCategoryId != model.CategoryId)
            {
                await _value.RemoveByProductId(model.Id);
                IEnumerable<DomainClasses.Entities.Attribute> attribute =
                    await _attribute.GetAttributesByCategoryId(model.CategoryId);
                _value.AddCategoryAttributesToProduct(attribute, model.Id);
            }
            await _unitOfWork.SaveChangesAsync();
            LuceneProducts.UpdateIndex(new ProductLuceneViewModel
            {
                Name = model.Name,
                Id = model.Id,
                Description = model.Description,
                ImagePath = model.PrincipleImagePath
            });
            CacheManager.InvalidateChildActionsCache();
            return RedirectToAction(MVC.Admin.Product.ActionNames.Index, MVC.Admin.Product.Name);
        }
        #endregion

        #region Delete
        [HttpPost]
        [Route("Delete")]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> Delete(Int64? id)
        {
            if (id == null)
                return Json(false);
            Boolean result = false;
            try
            {
                await _product.Delete(id.Value);
                await _unitOfWork.SaveChangesAsync();
                LuceneProducts.DeleteIndex(id.Value);
                CacheManager.InvalidateChildActionsCache();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return Json(result);
        }
        #endregion

        #region Fill Attribute
        [Route("{productId}/Attribut-Product")]
        [OutputCache(CacheProfile = "nostore")]
        [HttpGet]
        public virtual async Task<ActionResult> FillAttributes(Int64? productId)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            IEnumerable<FillProductAttributesViewModel> model = await _value.GetForUpdateValuesByProductId(productId.Value);
            if (model == null)
                return HttpNotFound();
            return View(MVC.Admin.Product.Views.FillAttributes, model);
        }

        [HttpPost]
        [Route("{ProductId}/Attribut-Product")]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        public virtual async Task<JsonResult> FillAttributes(IEnumerable<FillProductAttributesViewModel> model)
        {
            if (!ModelState.IsValid)
                return Json(false);

            await _value.UpdateValues(model);
            await _unitOfWork.SaveChangesAsync();
            return Json(true);
        }
        #endregion

        #region Picture 
        #region Add Picture
        [HttpGet]
        [Route("{productId:long}/Add-Picture")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> AddPictures(Int64? productId)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = await _product.GetById(productId.Value);

            if (product == null)
                return HttpNotFound();

            return View(MVC.Admin.Product.Views.AddPictures, new AddProductPicturesViewModel { ProductId = product.Id });
        }

        [HttpPost]
        [Route("{productId:long}/Add-Picture")]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        public virtual async Task<RedirectToRouteResult> AddPicture(AddProductPicturesViewModel model)
        {
            _productImage.Insert(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Product.ActionNames.Index, MVC.Admin.Product.Name);
        }
        #endregion
        #region Edit Picture
        [HttpGet]
        [OutputCache(CacheProfile = "nostore")]
        [Route("{ProductId}/Edit-Picture")]
        public virtual async Task<ActionResult> EditPictures(Int64? productId)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IEnumerable<ProductImage> m = await _productImage.GetImages(productId.Value);
            if (m == null)
                return HttpNotFound();
            return View(MVC.Admin.Product.Views.EditPictures, m);
        }

        [HttpPost]
        [Route("{ProductId}/Edit-Picture")]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> EditPicture(IEnumerable<ProductImage> model)
        {
            if (!ModelState.IsValid)
                return View(MVC.Admin.Product.Views.EditPictures, model);

            _productImage.Edit(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Product.ActionNames.Index, MVC.Admin.Product.Name);
        }
        #endregion
        #endregion

        #region Check Exist And NonAction
        [NonAction]
        private async Task PopulateCategoriesDropDownList(Int64? selectedItem)
        {
            IEnumerable<Category> category = await _category.GetFirstLevelCategories();
            ViewBag.Categories = new SelectList(category, "Id", "Name", selectedItem);
        }

        private async Task PopulateAllCategoriesDropDownList(Int64? selectedItem)
        {
            IEnumerable<Category> category = await _category.GetAll();
            ViewBag.Categories = new SelectList(category, "Id", "Name", selectedItem);
        }
        #endregion
    }
}