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
    public class OrderDetails : IOrderDetailsService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<OrderDetail> _order;
        #endregion

        #region Constructure
        public OrderDetails(IUnitOfWork unitOfWorks)
        {
            _unitOfWork = unitOfWorks;
            _order = _unitOfWork.Set<OrderDetail>();
        }
        #endregion

        #region Methods
        public void Add(OrderDetail orderDetail)
        {
            _order.Add(orderDetail);
        }

        public void Delete(Int64 id)
        {
            _order.Where(x => x.Id.Equals(id)).Delete();
        }

        public void DeleteByOrderId(Int64 orderId)
        {
            _order.Where(x => x.OrderId.Equals(orderId)).Delete();
        }

        public OrderDetail GetById(Int64 id) =>
            _order.Find(id);

        public IEnumerable<OrderDetail> GetDataTable(out Int32 total, Int32 page, Int32 count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetList() =>
            _order.AsNoTracking().ToList();

        public IEnumerable<OrderDetail> GetListByOrderId(Int64 orderId) =>
            _order.AsNoTracking().Where(x => x.OrderId.Equals(orderId)).ToList();

        public void Update(OrderDetail orderDetail)
        {
            _unitOfWork.MarkAsChanged(orderDetail);
        }
        #endregion
    }
}
