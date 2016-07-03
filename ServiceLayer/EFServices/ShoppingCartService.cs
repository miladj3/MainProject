using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DataLayer.Context;
using System.Data.Entity;
using EntityFramework.Extensions;

namespace ServiceLayer.EFServices
{
    class ShoppingCartService : IShoppingCartService
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWOrk;
        private readonly IDbSet<ShoppingCart> _shopCart;
        private readonly IProductService _product;
        #endregion

        #region Constracture
        public ShoppingCartService(IUnitOfWork unitOfWork, IProductService ProductService)
        {
            _unitOfWOrk = unitOfWork;
            _product = ProductService;
            _shopCart = _unitOfWOrk.Set<ShoppingCart>();
        }
        #endregion

        #region Method

        public void Add(ShoppingCart cart)
        {
            _shopCart.Add(cart);
        }

        public async Task<Int32> Delete(String cartId)
        {
            IQueryable<ShoppingCart> shopCarts = _shopCart.Where(x => x.CartNumber.Equals(cartId));
            shopCarts.AsNoTracking().ToList().ForEach(a =>
            {
                _product.DecreaseReserve(a.Id, a.Quantity);
            });
            return await shopCarts.DeleteAsync();
        }

        public Decimal DeleteItem(Int64 productId, String cartId)
        {
            ShoppingCart product = _shopCart.SingleOrDefault(x => x.ProductId.Equals(productId) && x.CartNumber.Equals(cartId));
            if (product == null)
                return Decimal.Zero;
            Decimal val = product.Quantity;
            _shopCart.Where(x => x.ProductId.Equals(productId) && x.CartNumber.Equals(cartId)).Delete();
            return val;
        }

        public ShoppingCart GetById(Int64 id) =>
            _shopCart.Find(id);

        public ShoppingCart GetCartItem(Int64 productId, String cartId) =>
            _shopCart.FirstOrDefault(x => x.ProductId.Equals(productId) && x.CartNumber.Equals(cartId));

        public IEnumerable<ShoppingCart> List(String cartId) =>
            _shopCart.AsNoTracking().Where(x => x.CartNumber.Equals(cartId)).ToList();

        public Decimal TotalValueInCart(String cartId) =>
            _shopCart.Where(x => x.CartNumber.Equals(cartId)).Sum(x => x.Quantity);


        public void Update(ShoppingCart cart)
        {
            _unitOfWOrk.MarkAsChanged(cart);
        }
        #endregion
    }
}
