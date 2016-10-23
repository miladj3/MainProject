using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using MVCUI.Extentions;
using MVCUI.Filters;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Attribute;
using ViewModel.ViewModel.Admin.Category;

namespace MVCUI.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Category")]
    [Route("{action}")]
    [T4MVC]
    [SiteAuthorize(Roles ="admin")]
    public partial class CategoryController : Controller
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _category;
        private readonly IAttributeService _attribute;
        #endregion

        #region Canstracture
        public CategoryController(IUnitOfWork unitOfWork,
                                                IAttributeService attribute,
                                                ICategoryService category)
        {
            this._unitOfWork = unitOfWork;
            this._category = category;
            this._attribute = attribute;
        }
        #endregion

        #region Index
        [HttpGet]
        [Route("Index")]
        //[OutputCache(CacheProfile ="long")]
        public virtual ActionResult Index() => View();

        [HttpGet]
        [Route("List")]
        [OutputCache(NoStore = false, Duration = 0, Location = System.Web.UI.OutputCacheLocation.None, VaryByParam = "*")]
        public virtual ActionResult List(String term = "", Int32 page = 1)
        {
            Int32 total;
            IEnumerable<ShowCatetoryViewModel> category = _category.GetDataTable(out total, term, page, 10);
            ViewBag.TotalCategories = total;
            ViewBag.PageNumber = page;
            return View(MVC.Admin.Category.Views.List, category);
        }
        #endregion

        #region Edit
        [HttpGet]
        //[OutputCache(CacheProfile ="long")]
        [Route("Edit/{id:long}")]
        public virtual async Task<ActionResult> Edit(Int64? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditCategoryViewModel category = await _category.GetForEdit(id.Value);
            if (category == null)
                return HttpNotFound();
            CacheManager.InvalidateChildActionsCache();
            return View(MVC.Admin.Category.Views.Edit, category);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Edit/{id}")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا تمام خانه ها رو با دقت پرنمایید");
                return View(MVC.Admin.Category.Views.Edit, model);
            }
            EditCategoryStatus status = await _category.Edit(model);
            switch (status)
            {
                case EditCategoryStatus.EditSuccessfully:
                    await _unitOfWork.SaveChangesAsync();
                    CacheManager.InvalidateChildActionsCache();
                    return RedirectToAction(MVC.Admin.Category.ActionNames.Index, MVC.Admin.Category.Name);

                case EditCategoryStatus.CategoryNameExist:
                    ModelState.AddModelError("Name", "این نام قبلا ثبت شده است");
                    return View(MVC.Admin.Category.Views.Edit, model);

                default:
                    ModelState.AddModelError("", "لطفا تمام خانه ها رو با دقت پرنمایید");
                    return View(MVC.Admin.Category.Views.Edit, model);
            }
        }
        #endregion

        #region Attribute Add & List & edit & delete
        #region Add
        [HttpGet]
        //[OutputCache(CacheProfile ="long")]
        [Route("{categoryId}/Attribute/Add")]
        public virtual async Task<ActionResult> AddAttribute(Int64? categoryId)
        {
            if (categoryId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Category category = await _category.GetById(categoryId.Value);

            if (category == null)
                return HttpNotFound();

            await PopulateCategoriesDropDownListForAttribute(category.Id);

            ViewBag.CategoryName = category.Name;
            return View(MVC.Admin.Category.Views.AddAttribute, new AddAttributeViewModel() { CategoryId = category.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{categoryId}/Attribute/Add")]
        public virtual async Task<ActionResult> AddAttribute(AddAttributeViewModel model)
        {
            Int64 categoryId = model.CategoryId;
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesDropDownListForAttribute(categoryId);
                ModelState.AddModelError("", "لطفا بار دیگر تلاش کنید");
                return View(MVC.Admin.Category.Views.AddAttribute, model);
            }
            Boolean status = await _attribute.Add(model);
            switch (status)
            {
                case true:
                    if (model.CascadeAddForChildren)
                    {
                        Category category = await _category.GetCategoryWithChildrenById(model.CategoryId);
                        await AddAttributeToChildrenCascade(model, category);
                    }
                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(MVC.Admin.Category.ActionNames.AddAttribute, MVC.Admin.Category.Name, new { categoryId = categoryId });

                case false:
                    ModelState.AddModelError("Name", "این ویژگی قبلا برای این گروه ثبت شده است");
                    return View(MVC.Admin.Category.Views.AddAttribute, model);

                default:
                    await PopulateCategoriesDropDownListForAttribute(categoryId);
                    ModelState.AddModelError("", "لطفا بار دیگر تلاش کنید");
                    return View();
            }
        }
        #endregion

        #region List
        [HttpGet]
        [Route("{categoryId:long}/Attributes")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> AttributeList(Int64? categoryId)
        {
            if (categoryId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Category category = await _category.GetById(categoryId.Value);
            if (category == null)
                return HttpNotFound();

            IEnumerable<DomainClasses.Entities.Attribute> attribute = await _attribute.GetAttributesByCategoryId(categoryId.Value);
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = category.Name;
            return View(MVC.Admin.Category.Views.AttributeList, attribute);
        }
        #endregion

        #region Edit
        [Route("{id:long}/Attribute/Edit")]
        [HttpGet]
        //[OutputCache(CacheProfile ="long")]
        public virtual async Task<ActionResult> EditAttribute(Int64? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            DomainClasses.Entities.Attribute attribute = await _attribute.GetAttributeById(id.Value);
            if (attribute == null)
                return HttpNotFound();

            return View(MVC.Admin.Category.Views.EditAttribute, new EditAttributeViewModel
            {
                Name = attribute.Name,
                CategoryId = attribute.CategoryId,
                Id = attribute.Id
            });
        }

        [HttpPost]
        [Route("{id}/Attribute/Edit")]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> EditAttribute(EditAttributeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا حانه های مورد نظر با دقت نکمیل نمایید");
                return View(MVC.Admin.Category.Views.EditAttribute, model);
            }
            DomainClasses.Entities.Attribute attribute = await _attribute.GetAttributeById(model.Id);
            String oldName = attribute.Name;
            attribute.Name = model.Name;
            Boolean status = await _attribute.Edit(attribute);
            switch (status)
            {
                case false:
                    ModelState.AddModelError("Name", "این ویژگی قبلا برای این گروه ثبت شده است");
                    return View(MVC.Admin.Category.Views.EditAttribute, model);

                case true:
                    if (model.CascadeAddForChildren)
                    {
                        Category category = await _category.GetById(model.CategoryId);
                        await EditAttributeForChildrenCacade(oldName, model.Name, category);
                    }
                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(MVC.Admin.Category.ActionNames.AttributeList, MVC.Admin.Category.Name, new { categoryId = model.CategoryId });
                default:
                    ModelState.AddModelError("", "لطفا حانه های مورد نظر با دقت نکمیل نمایید");
                    return View(MVC.Admin.Category.Views.EditAttribute, model);
            }
        }

        [HttpPost]
        [Route("Attribute/DeleteAttribute")]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        public virtual async Task<JsonResult> DeleteAttribute(Int64? id)
        {
            if (id == null)
                return Json(false);

            DomainClasses.Entities.Attribute attribute = await _attribute.GetAttributeById(id.Value);
            if (attribute == null)
                return Json(false);

            Category category = await _category.GetCategoryWithChildrenById(id.Value);
            await _attribute.Delete(id.Value);
            await DeleteAttributeFromChildrenOfCategory(category, attribute.Name);
            return Json(true);
        }
        #endregion
        #endregion

        #region Create
        [HttpGet]
        [Route("Create")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> Create()
        {
            PopulateCategoriesDropDownList(await _category.GetFirstLevelCategories());
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "خانه های مورد نیاز را با دقت پر نمایید");
                return View(MVC.Admin.Category.Views.Create, model);
            }
            Category category = new Category
            {
                Description = model.Description,
                KeyWords = model.KeyWords,
                ParentId = model.ParentId == 0 ? null : model.ParentId,
                Name = model.Name,
                DisplayOrder = model.DisplayOrder,
                DiscountPercent = model.DiscountPercent
            };

            await _attribute.AddParentAttributeToChild(category.ParentId, category.Id);
            AddCategoryStatus status = await _category.Add(category);
            switch (status)
            {
                case AddCategoryStatus.AddSuccessfully:
                    await _unitOfWork.SaveChangesAsync();
                    CacheManager.InvalidateChildActionsCache();
                    return RedirectToAction(MVC.Admin.Category.ActionNames.Create, MVC.Admin.Category.Name);

                case AddCategoryStatus.CategoryNameExist:
                    ModelState.AddModelError("Name", "این نام قبلا ثبت شده است");
                    return View(MVC.Admin.Category.Views.Create, model);

                default:
                    ModelState.AddModelError("", "لطفا یکبار دیگر تلاش کنید.");
                    return View(MVC.Admin.Category.Views.Create, model);
            }
        }
        #endregion

        #region Delete 
        [HttpPost]
        [Route("Delete")]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Delete(Int64? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _category.Delete(id.Value);
            await _unitOfWork.SaveChangesAsync();
            CacheManager.InvalidateChildActionsCache();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region NonAction & Check Exists
        [NonAction]
        private void PopulateCategoriesDropDownList(IEnumerable<Category> categories, long? selectedId = null)
        {
            ViewBag.CategoriesForSelect = new SelectList(categories, "Id", "Name",
                selectedId);
        }

        [NonAction]
        private async Task PopulateCategoriesDropDownListForAttribute(Int64 id)
        {
            IEnumerable<Category> category = await _category.GetAll();
            ViewBag.CategoriesForSelect = new SelectList(category, "Id", "Name", id);
        }

        [HttpPost]
        [Route("CheckExistCategoryforAdd")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> CheckExistCategoryforAdd(String Name) =>
            await _category.CheckExistName(Name) ?
            Json(false) :
            Json(true);

        [HttpPost]
        [Route("CheckExistCategoryforEdit")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> CheckExistCategoryforEdit(String Name, Int64 Id) =>
            await _category.CheckExistName(Name, Id) ?
            Json(false) :
            Json(true);

        [NonAction]
        private async Task AddAttributeToChildrenCascade(AddAttributeViewModel model, Category category)
        {
            if (category == null)
                return;
            if (!category.Children.Any())
                return;
            IEnumerable<Category> children = await _category.GetByParenId(category.Id);

            foreach (Category item in children.Where(child => !_category.HasAttributeByName(model.Name, child.Id)))
            {
                model.CategoryId = item.Id;
                await _attribute.Add(model);
            }
        }

        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        [Route("AddCheckExistAttributeForCategory")]
        public virtual async Task<JsonResult> AddCheckExistAttributeForCategory(String Name, Int64 CategoryId) =>
            await _attribute.ExisByName(Name, CategoryId) ?
            Json(false) :
            Json(true);

        [NonAction]
        private async Task EditAttributeForChildrenCacade(String oldName, String name, Category category)
        {
            if (category == null)
                return;
            if (!category.Children.Any())
                return;
            IEnumerable<Category> children = await _category.GetByParenId(category.Id);
            foreach (Category item in children.Where(x => _category.HasAttributeByName(oldName, x.Id)))
                await _attribute.EditByCategoryId(oldName, name, item.Id);
        }

        [HttpPost]
        [Route("EditCheckExistAttributeForCategory")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> EditCheckExistAttributeForCategory(String Name, Int64 CategoryId, Int64 Id) =>
            await _attribute.ExisByName(name: Name, categoryId: CategoryId, id: Id) ?
            Json(false) :
            Json(true);

        [NonAction]
        private async Task DeleteAttributeFromChildrenOfCategory(Category category, String name)
        {
            if (category == null)
                return;
            if (!category.Children.Any())
                return;

            IEnumerable<Category> chilred = await _category.GetByParenId(category.Id);
            foreach (Category item in chilred)
                await _attribute.DeleteByCategoryIdAndAttribueName(item.Id, name);
        }
        #endregion
    }
}