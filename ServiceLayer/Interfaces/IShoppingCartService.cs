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
        IEnumerable<ShoppingCart> List(String cartId);
        ShoppingCart GetById(Int64 id);
        Decimal TotalValueInCart(String cartId);
        ShoppingCart GetCartItem(Int64 productId, String cartId);
        Decimal DeleteItem(Int64 productId, String cartId);
    }
}
