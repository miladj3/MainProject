using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IOrderDetailsService
    {
        void Add(OrderDetail orderDetail);
        void Delete(Int64 id);
        void DeleteByOrderId(Int64 orderId);
        void Update(OrderDetail orderDetail);
        IEnumerable<OrderDetail> GetListByOrderId(Int64 orderId);
        IEnumerable<OrderDetail> GetList();
        IEnumerable<OrderDetail> GetDataTable(out Int32 total, Int32 page, Int32 count);
        OrderDetail GetById(Int64 id);
    }
}
