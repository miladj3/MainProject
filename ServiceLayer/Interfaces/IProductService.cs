using DomainClasses.Entities;
using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Product;
using ViewModel.ViewModel.Admin.SiteMap;

namespace ServiceLayer.Interfaces
{
    public interface IProductService
    {
        Task IncreaseSell(Int64 id, Decimal value);
        Task DecreaseSell(Int64 id, Decimal value);
        Task IncreaseReserve(Int64 id, Decimal value);
        Task DecreaseReserve(Int64 id, Decimal value);
        Task IncreaseStock(Int64 id, Decimal value);
        Task SaveRating(Int64 id, Double value);
        IEnumerable<ProductSectionViewModel> GetBelovedProducts(Int32 _take);
        IEnumerable<ProductSectionViewModel> GetNewProducts(Int32 _take);
        IEnumerable<ProductSectionViewModel> GetMoreSellProducts(Int32 _take);
        IEnumerable<ProductSectionViewModel> GetMoreViewedProducts(Int32 _take);
        IEnumerable<ProductSectionViewModel>GetProductSelected(Int32 _take);
        IEnumerable<ProductSectionViewModel> GetSelecionProductOfCategory(Int64 categoryId, Int32 take);
        IEnumerable<ProductLuceneViewModel> GetAllForAddLucene();
        Task<IEnumerable<SiteMapShowViewModel>> GetAllForSiteMap();
        Task ChangeProductsCategoryById(Int64 lastId, Int64 newId);
        Task<Product> GetById(Int64 id);
        void Insert(Product product);
        Task Update(EditProductViewModel viewModel);
        Task<EditProductViewModel> GetForEdit(Int64 id);
        Task Delete(Int64 id);
        Task<Boolean> CanUserRate(Int64 productId, String userName);
        IEnumerable<ProductViewModel> DataList(out Int32 total, String term, Boolean deleted, Boolean freeSend, Int32 page,
            Int32 count, Int64 categoryId, ProductOrderBy productOrderBy, DomainClasses.Enums.Order order,
            ProductType productType);
        void AppendAttributes(AppendAttributeViewModel viewModel);
        IEnumerable<ProductSectionViewModel> DataListSearch(out Int32 total, String term, Int32 page,
            Int32 count, PSFilter filtr, Int64 categoryId);
        Task<Boolean> CanAddToShoppingCart(Int64 id, Decimal value);
        Task AddUserToLikedUsers(Int64 id, User user);
        Task<Boolean> CanUserAddToWishList(Int64 id, String userName);
        Task AddUserToWishList(Int64 id, User user);
        Task<ProductDetailsViewModel> GetForShowDetails(Int64 id);
        Task<ProductCompareViewModel> GetForAddToCompare(Int64 id);
        void AddBulkProduct(IEnumerable<Product> products);
        Task RemoveFromWishList(Int64 id, User user);
    }
}
