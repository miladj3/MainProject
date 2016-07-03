using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Category;

namespace ServiceLayer.EFServices
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Category> _categories;
        #endregion

        #region Constructor
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categories = _unitOfWork.Set<Category>();
        }
        #endregion

        #region  Methods
        public Boolean CheckExistName(String name) =>
            _categories.Any(category => category.Name == name);

        public Boolean CheckExistName(String name, Int64 id) =>
            _categories.Any(category => category.Id != id && category.Name == name);
        #endregion

        #region CRUD
        public IEnumerable<Category> GetSecondLevelCategories() =>
                                                _categories.AsNoTracking()
                                                        .Where(a => a.ParentId != null)
                                                        .ToList();

        public IEnumerable<Category> GetAll() =>
                                               _categories.AsNoTracking().ToList();

        public Category GetCategoryWithChildrenById(Int64 id) =>
                                             _categories.AsNoTracking()
                                                .Include(a => a.Children).FirstOrDefault(a => a.Id == id);

        public EditCategoryViewModel GetForEdit(Int64 id) =>
                                            _categories.Where(a => a.Id == id).Select(category =>
                                                new EditCategoryViewModel
                                                {
                                                    Id = category.Id,
                                                    Description = category.Description,
                                                    Name = category.Name,
                                                    DiscountPercent = category.DiscountPercent,
                                                    DisplayOrder = category.DisplayOrder,
                                                    KeyWords = category.KeyWords
                                                }).FirstOrDefault();

        public Category GetById(Int64 id) =>
                                                 _categories.Find(id);

        public Category GetByName(String name)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetFirstLevelCategories() =>
                                                  _categories.AsNoTracking()
                                                        .Where(a => a.ParentId == null)
                                                        .ToList();

        public IEnumerable<ShowCatetoryViewModel> GetDataTable(out Int32 total, String term, Int32 page, Int32 count = 10)
        {
            var selectedCategories = _categories.AsNoTracking().OrderBy(a => a.Id).AsQueryable();
            if (!String.IsNullOrEmpty(term))
            {
                selectedCategories = selectedCategories.Where(a => a.Name.Contains(term));
            }

            var totalQuery = selectedCategories.FutureCount();
            var query = selectedCategories.Skip((page - 1) * count).Take(count).Select(a => new ShowCatetoryViewModel
            {
                Name = a.Name,
                Id = a.Id,
                DiscountPercent = a.DiscountPercent
            }).Future();
            total = totalQuery.Value;
            var categories = query.ToList();
            return categories;
        }
        public void Delete(Int64 id)
        {
            var category = GetById(id);
            _unitOfWork.MarkAsDeleted(category);
        }

        public AddCategoryStatus Add(Category category)
        {
            if (CheckExistName(category.Name))
                return AddCategoryStatus.CategoryNameExist;

            _categories.Add(category);
            return AddCategoryStatus.AddSuccessfully;
        }

        public EditCategoryStatus Edit(EditCategoryViewModel category)
        {
            if (CheckExistName(category.Name, category.Id))
                return EditCategoryStatus.CategoryNameExist;

            var selectedCategory = GetById(category.Id);
            selectedCategory.Description = category.Description;
            selectedCategory.KeyWords = category.KeyWords;
            selectedCategory.Name = category.Name;
            selectedCategory.DisplayOrder = category.DisplayOrder;
            selectedCategory.DiscountPercent = category.DiscountPercent;
            selectedCategory.Id = category.Id;
            return EditCategoryStatus.EditSuccessfully;
        }
        public IEnumerable<Category> GetByParenId(Int64 parentId) =>
                                                      _categories.AsNoTracking()
                                                           .Where(a => a.ParentId == parentId)
                                                           .ToList();
        #endregion

        #region Attributes
        public bool HasAttributeByName(String attributeName, Int64 id) =>
                                                                  _categories.Include(a => a.Attributes)
                                                                        .Any(a => a.Id == id && a.Attributes.Any(b => b.Name == attributeName));
        #endregion

        #region Menu
        public IEnumerable<Category> GetCategoriesForMenu()=>
                                                      _categories.AsNoTracking().Include(a => a.Children).Where(a => a.ParentId == null)
                                                            .Cacheable().ToList();

        #endregion

    }
}
