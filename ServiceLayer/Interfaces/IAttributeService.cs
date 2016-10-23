using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Attribute;

namespace ServiceLayer.Interfaces
{
    public interface IAttributeService
    {
        Task Delete(Int64 id);
       Task<Boolean> Add(AddAttributeViewModel viewModel);
        Task EditByCategoryId(String oldName, String newName, Int64 categoryId);
        Task<Boolean> ExisByName(String name, Int64 categoryId);
        Task<Boolean> ExisByName(String name, Int64 id, Int64 categoryId);
        Task<IEnumerable<DomainClasses.Entities.Attribute>> GetAttributesByCategoryId(Int64 id);
        Task<DomainClasses.Entities.Attribute> GetAttributeById(Int64 id);
        Task<Boolean> Edit(DomainClasses.Entities.Attribute attribute);
        void Add(DomainClasses.Entities.Attribute attribute);
        Task DeleteByCategoryIdAndAttribueName(Int64 categoryId, String attributeName);
        Task AddParentAttributeToChild(Int64? parentId, Int64 categoryId);
    }
}
