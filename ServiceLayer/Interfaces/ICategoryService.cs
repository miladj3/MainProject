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
        IEnumerable<Category> GetCategoriesForMenu();
        Boolean HasAttributeByName(String attributeName, Int64 id);
        IList<Category> GetFirstLevelCategories();
        IEnumerable<Category> GetSecondLevelCategories();
        IEnumerable<ShowCatetoryViewModel> GetDataTable(out Int32 total, String term, Int32 page, Int32 count);
        AddCategoryStatus Add(Category category);
        EditCategoryStatus Edit(EditCategoryViewModel category);
        IEnumerable<Category> GetAll();
        EditCategoryViewModel GetForEdit(Int64 id);
        Category GetCategoryWithChildrenById(Int64 id);
        Category GetById(Int64 id);
        Category GetByName(String name);
        void Delete(Int64 id);
        Boolean CheckExistName(String name);
        Boolean CheckExistName(String name, Int64 id);

        IEnumerable<Category> GetByParenId(Int64 parentId);
    }
}
