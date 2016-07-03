using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IOrderService
    {
        void Add(Order order);
        void Delete(Int64 id);
        void Update(Order order);
        IEnumerable<Order> GetListByUserId(Int64 UserId);
        IEnumerable<Order> GetListById(Int64 id);
        IEnumerable<Order> DataTable(out Int32 total, Int32 page, Int32 count);
    }
}
