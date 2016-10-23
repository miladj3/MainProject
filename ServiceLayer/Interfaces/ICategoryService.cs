using DomainClasses.Entities;
using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Category;

namespace ServiceLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesForMenu();
        Boolean HasAttributeByName(String attributeName, Int64 id);
        Task<IList<Category>> GetFirstLevelCategories();
        Task<IEnumerable<Category>> GetSecondLevelCategories();
        IEnumerable<ShowCatetoryViewModel> GetDataTable(out Int32 total, String term, Int32 page, Int32 count);
        Task<AddCategoryStatus> Add(Category category);
        Task<EditCategoryStatus> Edit(EditCategoryViewModel category);
        Task<IEnumerable<Category>> GetAll();
        Task<EditCategoryViewModel> GetForEdit(Int64 id);
        Task<Category> GetCategoryWithChildrenById(Int64 id);
        Task<Category> GetById(Int64 id);
        Category GetByName(String name);
        Task Delete(Int64 id);
        Task<Boolean> CheckExistName(String name);
        Task<Boolean> CheckExistName(String name, Int64 id);
        Task<IEnumerable<Category>> GetByParenId(Int64 parentId);
        Task<String> GetCategoryName(Int64 categoryId);
        Task<IList<Int64>> GetCountCategoryForSiteMap();
    }
}
