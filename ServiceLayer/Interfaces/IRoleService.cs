using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IRoleService
    {
        Boolean CreateRole(String roleName, String description = "");
        void AddUserToRole(User user, String roleName);
        Boolean RoleExist(String roleName);
        void RemoveRole(String roleName);
        Task<IList<Role>> GetAllRolesAsync();
        IList<User> UsersInRole(String roleName);
        Role GetRoleByUserName(String userName);
        Task<Role> GetRoleByUserId(Int64 userId);
        void RemoveUserFromRole(String userName);
        void EditRoleForUser(String userName, String roleName);
        Role GetRoleByName(String roleName);
        Task<Role> GetRoleByRoleIdAsync(Int64 roleId);
    }
}
