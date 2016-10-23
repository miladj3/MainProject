using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DomainClasses.Enums;
using ViewModel.ViewModel.Admin.Product;
using DataLayer.Context;
using System.Data.Entity;
using EntityFramework.Extensions;
using EFSecondLevelCache;
using EntityFramework.Future;
using ViewModel.ViewModel.Admin.SiteMap;

namespace ServiceLayer.EFServices
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Product> _products;
        private readonly IValueService _valueService;
        #endregion

        #region Constructor
        public ProductService(IUnitOfWork unitOfWork, IValueService valueService)
        {
            _unitOfWork = unitOfWork;
            _products = _unitOfWork.Set<Product>();
            _valueService = valueService;
        }
        #endregion

        public async Task ChangeProductsCategoryById(Int64 lastId, Int64 newId)
        {
            var product = GetById(newId);
            if (product != null)
                await _products.Where(a => a.CategoryId == lastId).UpdateAsync(b => new Product { CategoryId = newId });
        }

        public async Task<Product> GetById(Int64 id) =>
            await _products.SingleOrDefaultAsync(x => x.Id == id);

        public void Insert(Product product)=>
            _products.Add(product);

        public async Task Update(EditProductViewModel viewModel)
        {
            Product product = await GetById(viewModel.Id);
            product.Name = viewModel.Name;
            product.ApplyCategoryDiscount = viewModel.ApplyCategoryDiscount;
            product.CategoryId = viewModel.CategoryId;
            product.Deleted = viewModel.Deleted;
            product.IsFreeShipping = viewModel.IsFreeShipping;
            product.Description = viewModel.Description;
            product.DiscountPercent = viewModel.DiscountPercent;
            product.MetaDescription = viewModel.MetaDescription;
            product.MetaKeywords = viewModel.MetaKeyWords;
            product.Price = viewModel.Price;
            product.NotificationStockMinimum = viewModel.NotificationStockMinimum;
            product.PrincipleImagePath = viewModel.PrincipleImagePath;
            product.Ratio = viewModel.Ratio;
            product.Stock = viewModel.Stock;
            product.Type = viewModel.Type;
            product.PriceAfterDiscount = viewModel.PriceAfterDiscount;
            product.ComingSoon = viewModel.ComingSoon;
            product.HomePage = viewModel.HomePage;
            product.SpecialSell = viewModel.SpecialSell;
        }

        public async Task<EditProductViewModel> GetForEdit(Int64 id) =>
             await _products.Where(a => a.Id.Equals(id)).Select(a => new EditProductViewModel
             {
                 ApplyCategoryDiscount = a.ApplyCategoryDiscount,
                 CategoryId = a.CategoryId,
                 Deleted = a.Deleted,
                 Description = a.Description,
                 DiscountPercent = a.DiscountPercent,
                 Id = a.Id,
                 IsFreeShipping = a.IsFreeShipping,
                 Name = a.Name,
                 Ratio = a.Ratio,
                 MetaKeyWords = a.MetaKeywords,
                 NotificationStockMinimum = a.NotificationStockMinimum,
                 Price = a.Price,
                 PrincipleImagePath = a.PrincipleImagePath,
                 Stock = a.Stock,
                 MetaDescription = a.MetaDescription,
                 Type = a.Type,
                 OldCategoryId = a.CategoryId,
                 PriceAfterDiscount = a.PriceAfterDiscount,
                 ComingSoon = a.ComingSoon,
                 HomePage = a.HomePage,
                 SpecialSell = a.SpecialSell
             }).FirstOrDefaultAsync();

        public async Task Delete(Int64 id) =>
            await _products.Where(a => a.Id.Equals(id)).DeleteAsync();

        public IEnumerable<ProductViewModel> DataList(out Int32 total, String term, Boolean deleted, Boolean freeSend, Int32 page, Int32 count, Int64 categoryId, ProductOrderBy productOrderBy, DomainClasses.Enums.Order order, ProductType productType)
        {
            IQueryable<Product> selectedProducts =
                _products.AsNoTracking()
                    .Include(a => a.Category)
                    .Include(a => a.Values)
                    .Include(a => a.Images)
                    .AsQueryable();

            if (deleted || freeSend)
                selectedProducts =
                    selectedProducts.Where(a => a.Deleted == deleted && a.IsFreeShipping == freeSend).AsQueryable();

            if (categoryId != 0)
                selectedProducts = selectedProducts.Where(a => a.CategoryId == categoryId).AsQueryable();

            if (productType != ProductType.All)
                selectedProducts = selectedProducts.Where(a => a.Type == productType).AsQueryable();

            if (!string.IsNullOrEmpty(term))
                selectedProducts = selectedProducts.Where(product => product.Name.Contains(term)).AsQueryable();

            if (order == DomainClasses.Enums.Order.Asscending)
            {
                switch (productOrderBy)
                {
                    case ProductOrderBy.Name:
                        selectedProducts = selectedProducts.OrderBy(product => product.Name).AsQueryable();
                        break;
                    case ProductOrderBy.DiscountPercent:
                        selectedProducts = selectedProducts.OrderBy(product => product.DiscountPercent).AsQueryable();
                        break;
                    case ProductOrderBy.NotificationStockMinimun:
                        selectedProducts = selectedProducts.OrderBy(product => product.NotificationStockMinimum).AsQueryable();
                        break;
                    case ProductOrderBy.Price:
                        selectedProducts = selectedProducts.OrderBy(product => product.Price).AsQueryable();
                        break;
                    case ProductOrderBy.ReserveCount:
                        selectedProducts = selectedProducts.OrderBy(product => product.Reserve).AsQueryable();
                        break;
                    case ProductOrderBy.SellCount:
                        selectedProducts = selectedProducts.OrderBy(product => product.SellCount).AsQueryable();
                        break;
                    case ProductOrderBy.StockCount:
                        selectedProducts = selectedProducts.OrderBy(product => product.Stock).AsQueryable();
                        break;
                    case ProductOrderBy.ViewCount:
                        selectedProducts = selectedProducts.OrderBy(product => product.ViewCount).AsQueryable();
                        break;
                }
            }
            else
            {
                switch (productOrderBy)
                {
                    case ProductOrderBy.Name:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.Name).AsQueryable();
                        break;
                    case ProductOrderBy.DiscountPercent:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.DiscountPercent).AsQueryable();
                        break;
                    case ProductOrderBy.NotificationStockMinimun:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.NotificationStockMinimum).AsQueryable();
                        break;
                    case ProductOrderBy.Price:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.Price).AsQueryable();
                        break;
                    case ProductOrderBy.ReserveCount:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.Reserve).AsQueryable();
                        break;
                    case ProductOrderBy.SellCount:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.SellCount).AsQueryable();
                        break;
                    case ProductOrderBy.StockCount:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.Stock).AsQueryable();
                        break;
                    case ProductOrderBy.ViewCount:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.ViewCount).AsQueryable();
                        break;
                }
            }
            FutureCount totalQuery = selectedProducts.FutureCount();
            FutureQuery<ProductViewModel> selectQuery = selectedProducts.Skip((page - 1) * count).Take(count)
                .Select(a => new ProductViewModel
                {
                    ApplyCategoryDiscount = a.ApplyCategoryDiscount,
                    CategoryName = a.Category.Name,
                    Deleted = a.Deleted,
                    DiscountPercent = a.DiscountPercent,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    Name = a.Name,
                    NotificationStockMinimum = a.NotificationStockMinimum,
                    Price = a.Price,
                    PrincipleImagePath = a.PrincipleImagePath,
                    Rate = a.Rate.TotalRating,
                    Ratio = a.Ratio,
                    ReserveCount = a.Reserve,
                    SellCount = a.SellCount,
                    Stock = a.Stock,
                    ViewCount = a.ViewCount,
                    AddedImages = a.Images.Any(),
                    CompletedAttributes = a.Values.Any(),
                    Notification = a.Stock - a.Reserve <= a.NotificationStockMinimum,
                    PriceAfterDiscount = a.PriceAfterDiscount
                }).Future();
            total = totalQuery.Value;
            List<ProductViewModel> products = selectQuery.ToList();
            return products;
        }

        public void AppendAttributes(AppendAttributeViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductSectionViewModel> GetBelovedProducts(Int32 _take) =>
            _products.AsNoTracking().Where(a => !a.Deleted).Include(a => a.Category)
                .OrderByDescending(a => a.Rate.TotalRaters)
                .ThenByDescending(a => a.Rate.AverageRating)
                .Skip(0)
                .Take(_take)
                .Select(a => new ProductSectionViewModel
                {
                    TotalDiscount = a.ApplyCategoryDiscount ?
                                             a.DiscountPercent + a.Category.DiscountPercent :
                                             a.DiscountPercent,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    Name = a.Name,
                    Price = a.Price,
                    PrincipleImagePath = a.PrincipleImagePath,
                    AvrageRate = a.Rate.AverageRating,
                    Ratio = a.Ratio,
                    TotalRaters = a.Rate.TotalRaters ?? 0,
                    SellCount = a.SellCount,
                    IsInStock = (a.Stock - a.Reserve) >= a.Ratio,
                    ViewCount = a.ViewCount,
                    PriceAfterDiscount = a.PriceAfterDiscount,
                    ComingSoon = a.ComingSoon
                }).Cacheable().ToList();

        public IEnumerable<ProductSectionViewModel> GetNewProducts(Int32 _take) =>
               _products.AsNoTracking()
                    .Where(a => !a.Deleted && a.Stock - a.Reserve >= a.Ratio)
                    .Include(a => a.Category)
                    .OrderByDescending(x => x.AddedDate)
                    .Skip(0)
                    .Take(_take)
                    .Select(a => new ProductSectionViewModel
                    {
                        TotalDiscount = a.ApplyCategoryDiscount ? 
                                                 a.DiscountPercent + a.Category.DiscountPercent :
                                                 a.DiscountPercent,
                        Id = a.Id,
                        IsFreeShipping = a.IsFreeShipping,
                        Name = a.Name,
                        Price = a.Price,
                        PrincipleImagePath = a.PrincipleImagePath,
                        AvrageRate = a.Rate.AverageRating,
                        Ratio = a.Ratio,
                        TotalRaters = a.Rate.TotalRaters ?? 0,
                        SellCount = a.SellCount,
                        IsInStock = a.Stock - a.Reserve >= a.Ratio,
                        ViewCount = a.ViewCount,
                        PriceAfterDiscount = a.PriceAfterDiscount,
                        ComingSoon = a.ComingSoon
                    }).Cacheable().ToList();

        public IEnumerable<ProductSectionViewModel> GetMoreSellProducts(Int32 _take) =>
            _products.AsNoTracking()
                .Where(a => !a.Deleted && a.Stock - a.Reserve >= a.Ratio)
                .Include(a => a.Category)
             .OrderByDescending(a => a.SellCount)
            .Skip(0)
            .Take(_take)
                .Select(a => new ProductSectionViewModel
                {
                    TotalDiscount = a.ApplyCategoryDiscount ? 
                                             a.DiscountPercent + a.Category.DiscountPercent : 
                                             a.DiscountPercent,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    Name = a.Name,
                    Price = a.Price,
                    PrincipleImagePath = a.PrincipleImagePath,
                    AvrageRate = a.Rate.AverageRating,
                    TotalRaters = a.Rate.TotalRaters ?? 0,
                    Ratio = a.Ratio,
                    SellCount = a.SellCount,
                    ViewCount = a.ViewCount,
                    PriceAfterDiscount = a.PriceAfterDiscount,
                    ComingSoon = a.ComingSoon,
                    IsInStock = true
                }).Cacheable().ToList();

        public IEnumerable<ProductSectionViewModel> GetMoreViewedProducts(Int32 _take) =>
              _products.AsNoTracking()
                    .Where(a => !a.Deleted && a.Stock - a.Reserve >= a.Ratio)
                    .Include(a => a.Category)
                    .OrderByDescending(a => a.ViewCount)
                    .Skip(0)
                    .Take(_take)
                    .Select(a => new ProductSectionViewModel
                    {
                        TotalDiscount = a.ApplyCategoryDiscount ? 
                                                 a.DiscountPercent + a.Category.DiscountPercent :
                                                 a.DiscountPercent,
                        Id = a.Id,
                        IsFreeShipping = a.IsFreeShipping,
                        Name = a.Name,
                        Price = a.Price,
                        PrincipleImagePath = a.PrincipleImagePath,
                        AvrageRate = a.Rate.AverageRating,
                        Ratio = a.Ratio,
                        TotalRaters = a.Rate.TotalRaters ?? 0,
                        SellCount = a.SellCount,
                        IsInStock = a.Stock - a.Reserve >= a.Ratio,
                        ViewCount = a.ViewCount,
                        PriceAfterDiscount = a.PriceAfterDiscount,
                        ComingSoon = a.ComingSoon
                    }).Cacheable().ToList();

        public IEnumerable<ProductSectionViewModel> GetProductSelected(Int32 _take) =>
             _products.AsNoTracking().
                Where(x => x.HomePage == true).
                Include(x => x.Category).
                OrderByDescending(x => x.ViewCount).
                Skip(0).
                Take(_take).
                Select(a => new ProductSectionViewModel
                {
                    TotalDiscount = a.ApplyCategoryDiscount ?
                                             a.DiscountPercent + a.Category.DiscountPercent :
                                             a.DiscountPercent,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    Name = a.Name,
                    Price = a.Price,
                    PrincipleImagePath = a.PrincipleImagePath,
                    AvrageRate = a.Rate.AverageRating,
                    Ratio = a.Ratio,
                    TotalRaters = a.Rate.TotalRaters ?? 0,
                    SellCount = a.SellCount,
                    IsInStock = a.Stock - a.Reserve >= a.Ratio,
                    ViewCount = a.ViewCount,
                    PriceAfterDiscount = a.PriceAfterDiscount,
                    ComingSoon = a.ComingSoon
                }).Cacheable().ToList();

        public IEnumerable<ProductSectionViewModel> DataListSearch(out Int32 total, String term, Int32 page,
            Int32 count, PSFilter filtr, Int64 categoryId)
        {
            var selectedProducts =
                _products.AsNoTracking().Where(a => !a.Deleted)
                    .Include(a => a.Category).AsQueryable();

            if (categoryId != 0)
                selectedProducts = selectedProducts.Where(a => a.CategoryId == categoryId).AsQueryable();

            if (!string.IsNullOrEmpty(term))
                selectedProducts = selectedProducts.Where(product => product.Name.Contains(term)).AsQueryable();
            switch (filtr)
            {
                case PSFilter.All:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.Id).AsQueryable();
                    break;
                case PSFilter.New:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.AddedDate).AsQueryable();
                    break;
                case PSFilter.MoreView:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.ViewCount).AsQueryable();
                    break;
                case PSFilter.MoreSell:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.SellCount).AsQueryable();
                    break;
                case PSFilter.Beloved:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.Rate.TotalRaters).ThenByDescending(a => a.Rate.AverageRating).AsQueryable();
                    break;
                case PSFilter.IsInStock:
                    selectedProducts =
                        selectedProducts.Where(a => a.Stock - a.Reserve >= a.Ratio)
                            .OrderByDescending(a => a.Id)
                            .AsQueryable();
                    break;
                case PSFilter.HasDiscount:
                    selectedProducts =
                        selectedProducts.Where(a => a.DiscountPercent + a.Category.DiscountPercent > 0)
                            .OrderByDescending(a => a.Id)
                            .AsQueryable();
                    break;
                case PSFilter.FreeSend:
                    selectedProducts =
                        selectedProducts.Where(a => a.IsFreeShipping)
                            .OrderByDescending(a => a.Id)
                            .AsQueryable();
                    break;
            }

            var totalQuery = selectedProducts.FutureCount();
            var selectQuery = selectedProducts.Skip((page - 1) * count).Take(count)
                .Select(a => new ProductSectionViewModel
                {
                    AvrageRate = a.Rate.AverageRating,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    IsInStock = a.Stock - a.Reserve >= a.Ratio,
                    Name = a.Name,
                    Price = a.Price,
                    TotalRaters = a.Rate.TotalRaters ?? 0,
                    Ratio = a.Ratio,
                    PrincipleImagePath = a.PrincipleImagePath,
                    SellCount = a.SellCount,
                    TotalDiscount = a.ApplyCategoryDiscount ? a.DiscountPercent + a.Category.DiscountPercent : a.DiscountPercent,
                    ViewCount = a.ViewCount,
                    PriceAfterDiscount = a.PriceAfterDiscount,
                    ComingSoon = a.ComingSoon
                }).Future();
            total = totalQuery.Value;
            var products = selectQuery.ToList();
            return products;
        }

        public IEnumerable<ProductLuceneViewModel> GetAllForAddLucene()
        {
            return _products.Include(a => a.Values).Select(a => new ProductLuceneViewModel
            {
                Name = a.Name,
                Id = a.Id,
                ImagePath = a.PrincipleImagePath,
                Description = a.Description
            }).ToList();
        }

        public async Task<IEnumerable<SiteMapShowViewModel>> GetAllForSiteMap() =>
            await _products.AsNoTracking().Select(x => new SiteMapShowViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).Distinct().ToListAsync();


        public async Task SaveRating(Int64 id, Double value)
        {
            Product product = await GetById(id);
            if (product == null) return;

            if (!product.Rate.TotalRaters.HasValue) product.Rate.TotalRaters = 0;
            if (!product.Rate.TotalRating.HasValue) product.Rate.TotalRating = 0;
            if (!product.Rate.AverageRating.HasValue) product.Rate.AverageRating = 0;

            product.Rate.TotalRaters++;
            product.Rate.TotalRating += value;
            product.Rate.AverageRating = product.Rate.TotalRating / product.Rate.TotalRaters;
        }

        public async Task<Boolean> CanAddToShoppingCart(Int64 id, Decimal value)
        {
            Product product = await GetById(id);
            if (product == null)
                return false;
            return product.Stock - product.Reserve >= product.Ratio * value;
        }

        public async Task IncreaseSell(Int64 id, Decimal value)
        {
            Product product = await GetById(id);
            if (product == null)
                return;
            product.SellCount += value;
        }

        public async Task DecreaseSell(Int64 id, Decimal value)
        {
            Product product = await GetById(id);
            if (product == null)
                return;
            product.SellCount -= value;
        }

        public async Task IncreaseReserve(Int64 id, Decimal value)
        {
            Product product = await GetById(id);
            if (product == null)
                return;
            product.Reserve += value;
        }

        public async Task DecreaseReserve(Int64 id, Decimal value)
        {
            Product product = await GetById(id);
            if (product == null)
                return;
            product.Reserve -= value;
        }

        public async Task IncreaseStock(Int64 id, Decimal value)
        {
            Product product = await GetById(id);

            if (product == null)
                return;
            product.Stock += value;
        }

        public async Task<Boolean> CanUserRate(Int64 productId, String userName)
        {
            Product product = await GetById(productId);
            return product.LikedUser.All(a => a.UserName != userName);
        }

        public async Task AddUserToLikedUsers(Int64 id, User user)
        {
            Product product = await GetById(id);
            product.LikedUser.Add(user);
        }

        public async Task<Boolean> CanUserAddToWishList(Int64 id, String userName)
        {
            Product product = await GetById(id);
            return product.UsersFavorite.All(a => a.UserName != userName);
        }

        public async Task AddUserToWishList(Int64 id, User user)
        {
            Product product = await GetById(id);
            product.UsersFavorite.Add(user);
        }
        public async Task<ProductCompareViewModel> GetForAddToCompare(Int64 id)
        {
            Product product = await _products.Where(a => a.Id == id).Include(a => a.Category).FirstOrDefaultAsync();
            String[] attributes = await _valueService.GetAttValueOfProduct(id);
            return new ProductCompareViewModel
            {
                Attributes = attributes,
                ProductName = product.Name,
                ImagePath = product.PrincipleImagePath,
                AvrageRate = product.Rate.AverageRating,
                Description = product.Description,
                ProductId = product.Id,
                TotalRaters = product.Rate.TotalRaters ?? 0,
                Price = product.Price,
                FreeSend = product.IsFreeShipping,
                Discount = product.ApplyCategoryDiscount ?
                                product.DiscountPercent + product.Category.DiscountPercent :
                                product.DiscountPercent,
                PriceAfterDiscount = product.PriceAfterDiscount
            };
        }

        public async Task<ProductDetailsViewModel> GetForShowDetails(Int64 id)
        {
            Product product = await _products
                .Where(a => a.Id == id)
                .Include(a => a.Images)
                .Include(a => a.Category)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync();
            product.ViewCount++;
            return new ProductDetailsViewModel
            {
                TotalDiscount =
                    product.ApplyCategoryDiscount
                        ? product.DiscountPercent + product.Category.DiscountPercent
                        : product.DiscountPercent,
                Id = product.Id,
                IsFreeShipping = product.IsFreeShipping,
                Name = product.Name,
                Price = product.Price,
                TotalRaters = product.Rate.TotalRaters ?? 0,
                CategoryId = product.CategoryId,
                CommentsCount = product.Comments.Count,
                AvrageRate = product.Rate.AverageRating,
                Ratio = product.Ratio,
                PrincipleImage = product.PrincipleImagePath,
                Images = product.Images.Select(a => a.Path).ToArray(),
                SellCount = product.SellCount,
                IsInStock = product.Stock - product.Reserve >= product.Ratio,
                ViewCount = product.ViewCount,
                Description = product.Description,
                ComingSoon = product.ComingSoon,
                PriceAfterDiscount = product.PriceAfterDiscount,
                MetaDescription = product.MetaDescription,
                MetaKeywords = product.MetaKeywords
            };
        }

        public void AddBulkProduct(IEnumerable<Product> products)
        {
            try
            {
                _unitOfWork.AutoDetectChangesEnabled();
                ((DbSet<Product>)_products).AddRange(products);
            }
            finally
            {
                _unitOfWork.AutoDetectChangesEnabled(false);
            }
        }

        public async Task RemoveFromWishList(Int64 id, User user)
        {
            Product product = await GetById(id);
            product.UsersFavorite.Remove(user);
        }

        public IEnumerable<ProductSectionViewModel> GetSelecionProductOfCategory(Int64 categoryId, Int32 take) => _products.AsNoTracking()
                    .Where(a => !a.Deleted && a.Stock - a.Reserve >= a.Ratio && a.CategoryId == categoryId)
                    .Include(a => a.Category)
                    .OrderBy(a => a.SellCount)
                    .Skip(0)
                    .Take(take)
                    .Select(a => new ProductSectionViewModel
                    {
                        TotalDiscount = a.ApplyCategoryDiscount ?
                                                 a.DiscountPercent + a.Category.DiscountPercent :
                                                 a.DiscountPercent,
                        Id = a.Id,
                        IsFreeShipping = a.IsFreeShipping,
                        Name = a.Name,
                        Price = a.Price,
                        TotalRaters = a.Rate.TotalRaters ?? 0,
                        PrincipleImagePath = a.PrincipleImagePath,
                        AvrageRate = a.Rate.AverageRating,
                        Ratio = a.Ratio,
                        SellCount = a.SellCount,
                        IsInStock = a.Stock - a.Reserve >= a.Ratio,
                        ViewCount = a.ViewCount,
                        PriceAfterDiscount = a.PriceAfterDiscount,
                        ComingSoon = a.ComingSoon,
                        categoryId = a.CategoryId
                    }).Cacheable().ToList();
    }
}
