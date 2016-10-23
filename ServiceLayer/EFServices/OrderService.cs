using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DataLayer.Context;
using System.Data.Entity;
using ViewModel.ViewModel.Admin.Order;

namespace ServiceLayer.EFServices
{
    public class OrderService : IOrderService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWorks;
        private readonly IDbSet<Order> _orderService;
        private readonly IOrderDetailsService _orderDetails;
        private readonly IUserService _user;
        #endregion

        #region Constructure
        public OrderService(IUnitOfWork unitOfWorks, 
                                        IUserService user, 
                                        IOrderDetailsService orderDetail)
        {
            _unitOfWorks = unitOfWorks;
            _orderService = _unitOfWorks.Set<Order>();
            _user = user;
            _orderDetails = orderDetail;
        }
        #endregion

        #region Methods

        public async Task Add(OrderShowViewModel viewModel,IEnumerable<ShoppingCart> shoppingCard)
        {
            User user = await _user.GetUserByUserName(viewModel.UserName);
            ICollection<OrderDetail> _orderDetailsModel = await _orderDetails.Add(shoppingCard);
            Order model = new Order
            {
                Address = viewModel.Address,
                BuyDate = viewModel.BuyDate,
                Buyer = user,
                DiscountPrice = viewModel.DiscountPrice,
                OrderDetails = _orderDetailsModel,
                PeymentType = viewModel.PeymentType,
                PostDate = viewModel.PostDate,
                Status = viewModel.Status,
                TotalPrice = viewModel.TotalPrice,
                TransactionCode = viewModel.TransactionCode
            };
            _orderService.Add(model);
        }

        public IEnumerable<Order> DataTable(out Int32 total, Int32 page, Int32 count)
        {
            throw new NotImplementedException();
        }

        public void Delete(Int64 id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetListById(Int64 id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetListByUserId(Int64 UserId)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
