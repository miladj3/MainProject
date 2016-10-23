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
                    Content = "توضیحات خود را وارد نمایید",
                    AttributeId = item.Id,
                    ProductId = productId
                });
        }

        public async Task<String[]> GetAttValueOfProduct(Int64 productId) =>
            await _value.Where(x => x.ProductId.Equals(productId)).Select(x => x.Attribute.Name + ":" + x.Content).ToArrayAsync();

        public async Task<IEnumerable<FillProductAttributesViewModel>> GetForUpdateValuesByProductId(Int64 productId) =>
            await _value.Include(x => x.Product)
            .Include(x => x.Attribute)
            .Where(x => x.ProductId.Equals(productId))
            .Select(x => new FillProductAttributesViewModel()
            {
                Name = x.Attribute.Name,
                Id = x.Id,
                Value = x.Content
            }).ToListAsync();

        public IEnumerable<AttributeValueViewModel> GetProductProperties(Int64 id) =>
            _value.Include(x => x.Attribute)
            .Include(x => x.Product)
            .Where(x => x.ProductId==id)
            .Select(x => new AttributeValueViewModel()
            {
                Name = x.Attribute.Name,
                Value = x.Content
            }).ToList();

        public void Insert(Value value)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveByProductId(Int64 productId)=>
            await _value.Where(x => x.ProductId.Equals(productId)).DeleteAsync();

        public async Task UpdateValues(IEnumerable<FillProductAttributesViewModel> values)
        {
            if (values == null)
                return;
            IEnumerable<FillProductAttributesViewModel> fillProducts = values as IList<FillProductAttributesViewModel> ?? values.ToList();
            var ids = fillProducts.Select(x => x.Id);
            IEnumerable<Value> selectedValue = await _value.Where(x => ids.Contains(x.Id)).ToListAsync();
            Int32 count = selectedValue.Count();
            for (int i = 0; i < count; i++)
                selectedValue.ElementAt(i).Content = fillProducts.ElementAt(i).Value;
        }
        #endregion
    }
}
