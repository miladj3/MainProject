using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DataLayer.Context;
using System.Data.Entity;

namespace ServiceLayer.EFServices
{
    public class RoleService : IRoleService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWorks;
        private readonly IDbSet<Role> _role;
        #endregion

        #region Constracture
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWorks = unitOfWork;
            _role = _unitOfWorks.Set<Role>();
        }
        #endregion

        #region Methods
        public void AddUserToRole(User user, String roleName)
        {
            throw new NotImplementedException();
        }

        public Boolean CreateRole(String roleName, String description = "")
        {
            throw new NotImplementedException();
        }

        public void EditRoleForUser(String userName, String roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Role>> GetAllRolesAsync() =>
           await _role.AsNoTracking().ToListAsync();

        public Role GetRoleByName(String roleName) =>
            _role.Where(x => x.Name.Equals(roleName)).FirstOrDefault();

        public async Task<Role> GetRoleByRoleIdAsync(Int64 roleId) =>
        await _role.SingleOrDefaultAsync(x => x.Id == roleId);

        public async Task<Role> GetRoleByUserId(Int64 userId) =>
           await _role.Where(role => role.Users.Where(user => user.id == userId).FirstOrDefault().id == userId)
                .FirstOrDefaultAsync();


        public Role GetRoleByUserName(String userName)
        {
            throw new NotImplementedException();
        }

        public void RemoveRole(String roleName)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromRole(String userName)
        {
            throw new NotImplementedException();
        }

        public Boolean RoleExist(String roleName)
        {
            throw new NotImplementedException();
        }

        public IList<User> UsersInRole(String roleName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
