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
        void Delete(Int64 id);
       Boolean Add(AddAttributeViewModel viewModel);
        void EditByCategoryId(String oldName, String newName, Int64 categoryId);
        Boolean ExisByName(String name, Int64 categoryId);
        Boolean ExisByName(String name, Int64 id, Int64 categoryId);
        IEnumerable<DomainClasses.Entities.Attribute> GetAttributesByCategoryId(Int64 id);
        DomainClasses.Entities.Attribute GetAttributeById(Int64 id);
        Boolean Edit(DomainClasses.Entities.Attribute attribute);
        void Add(DomainClasses.Entities.Attribute attribute);
        void DeleteByCategoryIdAndAttribueName(Int64 categoryId, String attributeName);
        void AddParentAttributeToChild(Int64? parentId, Int64 categoryId);
    }
}
