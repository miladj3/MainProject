using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.ViewModel.Admin.Attribute;
using DataLayer.Context;
using System.Data.Entity;
using EntityFramework.Extensions;

namespace ServiceLayer.EFServices
{
    public class AttributeService : IAttributeService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<DomainClasses.Entities.Attribute> _attribute;
        #endregion

        #region Constracture
        public AttributeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._attribute = _unitOfWork.Set<DomainClasses.Entities.Attribute>();
        }
        #endregion

        #region Check Exits
        public async Task<Boolean> ExisByName(String name, Int64 categoryId) =>
            await _attribute.AnyAsync(x => x.CategoryId == categoryId && x.Name == name);

        public async Task<Boolean> ExisByName(String name, Int64 id, Int64 categoryId) =>
            await _attribute.AnyAsync(x => x.CategoryId == categoryId && x.Id != id && x.Name == name);

        #endregion

        #region Add
        public void Add(DomainClasses.Entities.Attribute attribute)
        {
            _attribute.Add(attribute);
        }

        public async Task<Boolean> Add(AddAttributeViewModel viewModel)
        {
            if (await ExisByName(viewModel.Name, viewModel.CategoryId))
                return false;
            DomainClasses.Entities.Attribute attr = new DomainClasses.Entities.Attribute
            {
                CategoryId = viewModel.CategoryId,
                Name = viewModel.Name,
                Type = DomainClasses.Enums.AttributeType.Text
            };
            _attribute.Add(attr);
            return true;
        }

        public async Task AddParentAttributeToChild(Int64? parentId, Int64 categoryId)
        {
            if (parentId == null)
                return;

            IEnumerable<DomainClasses.Entities.Attribute> categoryAttributes = await GetAttributesByCategoryId(parentId.Value);
            IList<DomainClasses.Entities.Attribute> attributes = categoryAttributes as List<DomainClasses.Entities.Attribute> ?? categoryAttributes.ToList();

            if (!attributes.Any())
                return;

            foreach (DomainClasses.Entities.Attribute item in attributes)
                 Add(new DomainClasses.Entities.Attribute()
                {
                    Name = item.Name,
                    CategoryId = categoryId,
                    Type = item.Type
                });
        }
        #endregion

        #region Delete
        public async Task Delete(Int64 id)
        {
            await _attribute.Where(x => x.Id == id).DeleteAsync();
        }

        public async Task DeleteByCategoryIdAndAttribueName(Int64 categoryId, String attributeName)
        {
            await _attribute.Where(x => x.CategoryId == categoryId && x.Name == attributeName).DeleteAsync();
        }
        #endregion
        
        #region Edit
        public async Task<Boolean> Edit(DomainClasses.Entities.Attribute attribute)
        {
            if (await ExisByName(attribute.Name, attribute.Id, attribute.CategoryId))
                return false;
            _unitOfWork.MarkAsChanged(attribute);
            return true;
        }

        public async Task EditByCategoryId(String oldName, String newName, Int64 categoryId)
        {
            var attr = _attribute.Where(x => x.CategoryId == categoryId && x.Name == oldName);
            await attr.UpdateAsync(a => new DomainClasses.Entities.Attribute { Name = newName });
        }
        #endregion

        #region Get

        public async Task<DomainClasses.Entities.Attribute> GetAttributeById(Int64 id) =>
            await _attribute.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<DomainClasses.Entities.Attribute>> GetAttributesByCategoryId(Int64 id) =>
            await _attribute.AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();

        #endregion
    }
}
