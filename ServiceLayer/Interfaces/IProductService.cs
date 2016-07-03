using DomainClasses.Entities;
using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Product;

namespace ServiceLayer.Interfaces
{
    public interface IProductService
    {
        void IncreaseSell(Int64 id, Decimal value);
        void DecreaseSell(Int64 id, Decimal value);
        void IncreaseReserve(Int64 id, Decimal value);
        void DecreaseReserve(Int64 id, Decimal value);
        void IncreaseStock(Int64 id, Decimal value);
        void SaveRating(Int64 id, Double value);
        IEnumerable<ProductSectionViewModel> GetBelovedProducts();
        IEnumerable<ProductSectionViewModel> GetNewProducts();
        IEnumerable<ProductSectionViewModel> GetMoreSellProducts();
        IEnumerable<ProductSectionViewModel> GetMoreViewedProducts();
        IEnumerable<ProductSectionViewModel> GetSelecionProductOfCategory(Int64 categoryId);
        IEnumerable<ProductLuceneViewModel> GetAllForAddLucene();
        void ChangeProductsCategoryById(Int64 lastId, Int64 newId);
        Product GetById(Int64 id);
        void Insert(Product product);
        void Update(EditProductViewModel viewModel);
        EditProductViewModel GetForEdit(Int64 id);
        void Delete(Int64 id);
        bool CanUserRate(Int64 productId, String userName);
        IEnumerable<ProductViewModel> DataList(out Int32 total, String term, Boolean deleted, Boolean freeSend, Int32 page,
            Int32 count, Int64 categoryId, ProductOrderBy productOrderBy, DomainClasses.Enums.Order order,
            ProductType productType);
        void AppendAttributes(AppendAttributeViewModel viewModel);

        IEnumerable<ProductSectionViewModel> DataListSearch(out Int32 total, String term, Int32 page,
            Int32 count, PSFilter filtr, Int64 categoryId);

        bool CanAddToShoppingCart(Int64 id, Decimal value);

        void AddUserToLikedUsers(Int64 id, User user);
        bool CanUserAddToWishList(Int64 id, String userName);
        void AddUserToWishList(Int64 id, User user);

        ProductDetailsViewModel GetForShowDetails(Int64 id);
        ProductCompareViewModel GetForAddToCompare(Int64 id);
        void AddBulkProduct(IEnumerable<Product> products);


        void RemoveFromWishList(Int64 id, User user);
    }
}
