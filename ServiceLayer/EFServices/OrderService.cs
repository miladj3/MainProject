using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DataLayer.Context;
using System.Data.Entity;

namespace ServiceLayer.EFServices
{
    public class OrderService : IOrderService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWorks;
        private readonly IDbSet<OrderService> _orderService;
        #endregion

        #region Construcure
        public OrderService(IUnitOfWork unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
            _orderService = _unitOfWorks.Set<OrderService>();
        }
        #endregion

        #region Methods

        public void Add(Order order)
        {
            throw new NotImplementedException();
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
