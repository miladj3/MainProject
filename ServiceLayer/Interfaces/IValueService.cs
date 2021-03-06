﻿using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Product;

namespace ServiceLayer.Interfaces
{
    public interface IValueService
    {
        void Insert(Value value);
        void AddCategoryAttributesToProduct(IEnumerable<DomainClasses.Entities.Attribute> attributes, Int64 productId);
        Task<IEnumerable<FillProductAttributesViewModel>> GetForUpdateValuesByProductId(Int64 productId);
        Task UpdateValues(IEnumerable<FillProductAttributesViewModel> values);
        Task RemoveByProductId(Int64 productId);
        IEnumerable<AttributeValueViewModel> GetProductProperties(Int64 id);
        Task<String[]> GetAttValueOfProduct(Int64 productId);
    }
}
