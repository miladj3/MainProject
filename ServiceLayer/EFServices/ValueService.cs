using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.ViewModel.Admin.Product;
using System.Data.Entity;
using DataLayer.Context;
using EntityFramework.Extensions;

namespace ServiceLayer.EFServices
{
    public class ValueService : IValueService
    {
        #region Fields
        private readonly IDbSet<Value> _value;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Contracture
        public ValueService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            _value = _unitOfWork.Set<Value>();
        }
        #endregion

        #region Method Of CRUD
        public void AddCategoryAttributesToProduct(IEnumerable<DomainClasses.Entities.Attribute> attributes, Int64 productId)
        {
            foreach (DomainClasses.Entities.Attribute item in attributes)
                _value.Add(new Value()
                {
                    Content = "---",
                    AttributeId = item.Id,
                    ProductId = productId
                });
        }

        public String[] GetAttValueOfProduct(Int64 productId) =>
            _value.Where(x => x.ProductId.Equals(productId)).Select(x => x.Attribute.Name + ":" + x.Content).ToArray();

        public IEnumerable<FillProductAttributesViewModel> GetForUpdateValuesByProductId(Int64 productId) =>
            _value.Include(x => x.Product)
            .Include(x => x.Attribute)
            .Where(x => x.ProductId.Equals(productId))
            .Select(x => new FillProductAttributesViewModel()
            {
                Name = x.Attribute.Name,
                Id = x.Id,
                Value = x.Content
            }).ToList();

        public IEnumerable<AttributeValueViewModel> GetProductProperties(Int64 id) =>
            _value.Include(x => x.Attribute)
            .Include(x => x.Product)
            .Where(x => x.ProductId.Equals(id))
            .Select(x => new AttributeValueViewModel()
            {
                Name = x.Attribute.Name,
                Value = x.Content
            }).ToList();

        public void Insert(Value value)
        {
            throw new NotImplementedException();
        }

        public void RemoveByProductId(Int64 productId)
        {
            _value.Where(x => x.ProductId.Equals(productId)).Delete();
        }

        public void UpdateValues(IEnumerable<FillProductAttributesViewModel> values)
        {
            if (values == null)
                return;
            var fillProducts = values as IList<FillProductAttributesViewModel> ?? values.ToList();
            var ids = fillProducts.Select(x => x.Id);
            var selectedValue = _value.Where(x => ids.Contains(x.Id)).ToList();
            for (int i = 0; i < selectedValue.Count; i++)
                selectedValue.ElementAt(i).Content = fillProducts.ElementAt(i).Value;
        }
        #endregion
    }
}
