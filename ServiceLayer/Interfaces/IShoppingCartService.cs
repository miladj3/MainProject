using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IShoppingCartService
    {
        void Add(ShoppingCart cart);
        Task<Int32> Delete(String cartId);
        void Update(ShoppingCart cart);
        Task<IEnumerable<ShoppingCart>> List(String cartId, Boolean IsAsNoTracking=true);
        IEnumerable<ShoppingCart> ListNoAsync(String cartId, Boolean IsAsNoTracking = true);
        ShoppingCart GetById(Int64 id);
        Task<Decimal> TotalValueInCart(String cartId);
        Task<ShoppingCart> GetCartItem(Int64 productId, String cartId);
        Task<Tuple<Decimal, Int64>> DeleteItem(Int64 shoppingId, String cartId);
    }
}
