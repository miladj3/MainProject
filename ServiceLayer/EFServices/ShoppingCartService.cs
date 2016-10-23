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
    public class ShoppingCartService : IShoppingCartService
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

        public void Add(ShoppingCart cart) =>
            _shopCart.Add(cart);

        /// <summary>
        /// Delete All Item From User
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task<Int32> Delete(String cartId)
        {
            IQueryable<ShoppingCart> shopCarts = _shopCart.Where(x => x.CartNumber == cartId);
            shopCarts.AsNoTracking().ToList().ForEach(a =>
            {
                _product.DecreaseReserve(a.Id, a.Quantity);
            });
            return await shopCarts.DeleteAsync();
        }

        public async Task<Tuple<Decimal, Int64>> DeleteItem(Int64 shoppingId, String cartId)
        {
            ShoppingCart model = await _shopCart.SingleOrDefaultAsync(x => x.Id == shoppingId && x.CartNumber == cartId);
            if (model == null)
                return new Tuple<Decimal, Int64>(Decimal.Zero, 0);
            Decimal val = model.Quantity;
            await _shopCart.Where(x => x.Id == shoppingId && x.CartNumber == cartId).DeleteAsync();
            return new Tuple<Decimal, Int64>(val, model.ProductId);
        }

        public ShoppingCart GetById(Int64 id) =>
            _shopCart.Find(id);

        public async Task<ShoppingCart> GetCartItem(Int64 productId, String cartId) =>
            await _shopCart.FirstOrDefaultAsync(x => x.ProductId == productId && x.CartNumber == cartId);

        public async Task<IEnumerable<ShoppingCart>> List(String cartId, Boolean IsAsNoTracking = true)
        {
            IQueryable<ShoppingCart> model = IsAsNoTracking ?
                                                                    _shopCart.AsNoTracking().AsQueryable() :
                                                                    _shopCart.AsQueryable();
            model = model.Where(x => x.CartNumber == cartId && x.isComplete == false);
           return await model.ToListAsync();
        }
        public IEnumerable<ShoppingCart> ListNoAsync(String cartId, Boolean IsAsNoTracking = true)
        {
            IQueryable<ShoppingCart> model = IsAsNoTracking ? 
                                                                _shopCart.AsNoTracking().AsQueryable() : 
                                                                _shopCart.AsQueryable();
            model = model.Where(x => x.CartNumber == cartId && x.isComplete == false);
            return model.ToList();
        }
        public async Task<Decimal> TotalValueInCart(String cartId)
        {
            try
            {
                return await _shopCart.Where(x => x.CartNumber == cartId && x.isComplete == false).SumAsync(x => x.Quantity);
            }
            catch (Exception)
            {
                return default(Decimal);
            }
        }

        public void Update(ShoppingCart cart) =>
            _unitOfWOrk.MarkAsChanged(cart);

        #endregion
    }
}
