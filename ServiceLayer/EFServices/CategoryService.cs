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
        public async Task<Boolean> CheckExistName(String name) =>
           await _categories.AnyAsync(category => category.Name == name);

        public async Task<Boolean> CheckExistName(String name, Int64 id) =>
           await _categories.AnyAsync(category => category.Id != id && category.Name == name);
        #endregion

        #region CRUD
        public async Task<IEnumerable<Category>> GetSecondLevelCategories() =>
                                                await _categories.AsNoTracking()
                                                        .Where(a => a.ParentId != null)
                                                        .ToListAsync();

        public async Task<IEnumerable<Category>> GetAll() =>
                                            await _categories.AsNoTracking().ToListAsync();

        public async Task<Category> GetCategoryWithChildrenById(Int64 id) =>
                                            await _categories.AsNoTracking()
                                                .Include(a => a.Children).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<EditCategoryViewModel> GetForEdit(Int64 id) =>
                                            await _categories.Where(a => a.Id == id).Select(category =>
                                                new EditCategoryViewModel
                                                {
                                                    Id = category.Id,
                                                    Description = category.Description,
                                                    Name = category.Name,
                                                    DiscountPercent = category.DiscountPercent,
                                                    DisplayOrder = category.DisplayOrder,
                                                    KeyWords = category.KeyWords
                                                }).FirstOrDefaultAsync();

        public async Task<Category> GetById(Int64 id) =>
                                                await _categories.SingleOrDefaultAsync(x => x.Id == id);

        public Category GetByName(String name)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Category>> GetFirstLevelCategories() =>
                                                await _categories.AsNoTracking()
                                                        .Where(a => a.ParentId == null)
                                                        .ToListAsync();



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
        public async Task Delete(Int64 id)
        {
            Category category = await GetById(id);
            _unitOfWork.MarkAsDeleted(category);
        }

        public async Task<AddCategoryStatus> Add(Category category)
        {
            if (await CheckExistName(category.Name))
                return AddCategoryStatus.CategoryNameExist;

            _categories.Add(category);
            return AddCategoryStatus.AddSuccessfully;
        }

        public async Task<EditCategoryStatus> Edit(EditCategoryViewModel category)
        {
            if (await CheckExistName(category.Name, category.Id))
                return EditCategoryStatus.CategoryNameExist;

            Category selectedCategory = await GetById(category.Id);
            selectedCategory.Description = category.Description;
            selectedCategory.KeyWords = category.KeyWords;
            selectedCategory.Name = category.Name;
            selectedCategory.DisplayOrder = category.DisplayOrder;
            selectedCategory.DiscountPercent = category.DiscountPercent;
            selectedCategory.Id = category.Id;
            return EditCategoryStatus.EditSuccessfully;
        }
        public async Task<IEnumerable<Category>> GetByParenId(Int64 parentId) =>
                                                     await _categories.AsNoTracking()
                                                           .Where(a => a.ParentId == parentId)
                                                           .ToListAsync();

        public async Task<String> GetCategoryName(Int64 categoryId)
        {
            Category model = await GetById(categoryId);
            return model.Name;
        }

        public async Task<IList<Int64>> GetCountCategoryForSiteMap()
        {
            var t= await _categories.AsNoTracking().Select(x => new { id = x.Id }).ToListAsync();
            IList<Int64> model = new List<Int64>();
            foreach (var item in t)
                model.Add(item.id);
            return model;
        }
        #endregion

        #region Attributes
        public bool HasAttributeByName(String attributeName, Int64 id) =>
                                                                  _categories.Include(a => a.Attributes)
                                                                        .Any(a => a.Id == id && a.Attributes.Any(b => b.Name == attributeName));
        #endregion

        #region Menu
        public async Task<IEnumerable<Category>> GetCategoriesForMenu() =>
              await _categories
            .AsNoTracking()
            .Include(a => a.Children)
            .Where(a => a.ParentId == null)
            .Cacheable().ToListAsync();

        #endregion

    }
}
