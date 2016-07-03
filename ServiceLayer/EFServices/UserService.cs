using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DomainClasses.Enums;
using System.Linq.Expressions;
using ViewModel.ViewModel.Admin.Product;
using ViewModel.ViewModel.Admin.User;
using DataLayer.Context;
using System.Data.Entity;
using Utilities.Security;
using EntityFramework.Extensions;

namespace ServiceLayer.EFServices
{
    public class UserService : IUserService
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _user;
        #endregion

        #region Constracture
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _user = _unitOfWork.Set<User>();
        }
        #endregion

        #region Private Member
        private static VerifyUserStatus Verify(User selectedUser, String password)
        {
            var result = VerifyUserStatus.VerifiedFaild;
            Boolean verifyPass = Encryption.VerifyPassword(password, selectedUser.Password);
            if (verifyPass)
            {
                if (selectedUser.IsBaned)
                    result = VerifyUserStatus.UserIsBaned;
                else
                {
                    selectedUser.LastLoginDate = DateTime.Now;
                    result = VerifyUserStatus.VerifiedSuccessfully;
                }
            }
            return result;
        }
        #endregion

        #region CRUD



        public AddUserStatus Add(User user)
        {
            if (ExistsByPhoneNumber(user.PhoneNumber, user.id))
                return AddUserStatus.PhoneNumberExist;
            if (ExistsByUserName(user.UserName))
                return AddUserStatus.UserNameExist;
            _user.Add(user);
            return AddUserStatus.AddingUserSuccessfully;
        }
        public EditedUserStatus EditUser(User user)
        {
            if (ExistsByPhoneNumber(user.PhoneNumber, user.id))
                return EditedUserStatus.PhoneNumberExist;
            if (ExistsByUserName(user.UserName))
                return EditedUserStatus.UserNameExist;
            User selectedUser = GetUserById(user.id);
            if (user.Password != null)
                selectedUser.Password = user.Password;
            selectedUser.UserName = user.UserName;
            selectedUser.FirstName = user.FirstName;
            selectedUser.LastName = user.LastName;
            selectedUser.IsBaned = user.IsBaned;
            selectedUser.PhoneNumber = user.PhoneNumber;
            selectedUser.Role = user.Role;
            return EditedUserStatus.UpdatingUserSuccessfully;
        }
        public IList<UserViewModel> GetDataTable(out Int32 total,
                                                                                    String term,
                                                                                    Int32 page,
                                                                                    Int32 count,
                                                                                    DomainClasses.Enums.Order order,
                                                                                    UserOrderBy orderBy,
                                                                                    UserSearchBy searchBy)
        {
            IQueryable<User> selectedUsers = _user.AsNoTracking().Include(a => a.Role).AsQueryable();

            if (!String.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case UserSearchBy.UserName:
                        selectedUsers = selectedUsers.Where(user => user.UserName.Contains(term)).AsQueryable();
                        break;
                    case UserSearchBy.RoleDescription:
                        selectedUsers = selectedUsers.Where(user => user.Role.Description.Contains(term)).AsQueryable();
                        break;
                    case UserSearchBy.PhoneNumber:
                        selectedUsers =
                            selectedUsers.Where(user => user.PhoneNumber.Contains(term)).AsQueryable();
                        break;
                    case UserSearchBy.Ip:
                        selectedUsers =
                            selectedUsers.Where(user => user.IP.Contains(term)).AsQueryable();
                        break;
                }
            }


            if (order == DomainClasses.Enums.Order.Asscending)
            {
                switch (orderBy)
                {
                    case UserOrderBy.UserName:
                        selectedUsers = selectedUsers.OrderBy(user => user.UserName).AsQueryable();
                        break;
                    case UserOrderBy.OrderCount:
                        selectedUsers = selectedUsers.OrderBy(user => user.Orders.Count).AsQueryable();
                        break;
                    case UserOrderBy.RegisterDate:
                        selectedUsers = selectedUsers.OrderBy(user => user.RegisterDate).AsQueryable();
                        break;
                }
            }
            else
            {
                switch (orderBy)
                {
                    case UserOrderBy.UserName:
                        selectedUsers = selectedUsers.OrderByDescending(user => user.UserName).AsQueryable();
                        break;
                    case UserOrderBy.OrderCount:
                        selectedUsers = selectedUsers.OrderByDescending(user => user.Orders.Count).AsQueryable();
                        break;
                    case UserOrderBy.RegisterDate:
                        selectedUsers = selectedUsers.OrderByDescending(user => user.RegisterDate).AsQueryable();
                        break;
                }
            }
            var totalQuery = selectedUsers.FutureCount();
            var selectQuery = selectedUsers.Skip((page - 1) * count).Take(count)
                .Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                    FullName = x.FirstName + " " + x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    RegisterType = x.RegisterType == UserRegisterType.Active ? "خرید کرده" : "خرید نکرده",
                    Baned = x.IsBaned,
                    CommentCount = x.Comments.Count,
                    Id = x.id,
                    OrderCount = x.Orders.Count,
                    RoleDescritpion = x.Role.Description
                }).Future();
            total = totalQuery.Value;
            var users = selectQuery.ToList();
            return users;
        }
        
        public DetailsUserViewModel GetUserDetail(Int64 id) =>
            _user.AsNoTracking().Where(x => x.id.Equals(id))
            .Include(x => x.Role)
            .Select(x => new DetailsUserViewModel()
            {
                CommentsCount = x.Comments.Count,
                FirstName = x.FirstName,
                LastName = x.LastName,
                IP = x.IP,
                OrdersCount = x.Orders.Count,
                PhoneNumber = x.PhoneNumber,
                RegisterType =
                                    x.RegisterType == UserRegisterType.Active
                                        ? "وارد سایت شده است"
                                        : "وارد سایت نشده است",
                RoleName = x.Role.Description,
                UserName = x.UserName
            }).FirstOrDefault();
        #endregion

        #region Authentication
        public Boolean ExistsByPhoneNumber(String phoneNumber) =>
            _user.Any(x => x.PhoneNumber.Equals(phoneNumber));

        public Boolean ExistsByPhoneNumber(String phoneNumber, Int64 id) =>
            _user.Any(x => x.PhoneNumber.Equals(phoneNumber) && x.id != id);

        public Boolean ExistsByUserName(String userName) =>
            _user.Any(x => x.UserName.Equals(userName));

        public Boolean ExistsByUserName(String userName, Int64 id) =>
            _user.Any(x => x.UserName.Equals(userName) && x.id != id);

        public String GeneratePassword()
        {
            String _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random R = new Random();
            int _passLength = 6;
            Char[] ch = new Char[_passLength];
            int allowCharsCount = _allowedChars.Length;
            for (int i = 0; i == _passLength; i++)
                ch[i] = _allowedChars[(Int32)((allowCharsCount) * R.NextDouble())];
            return new String(ch);
        }


        public VerifyUserStatus VerifyUserByPhoneNumber(String phoneNumber,
                                                                                            String password,
                                                                                            ref String correctUserName,
                                                                                            ref Int64 userId,
                                                                                            String ip)
        {
            User _u = _user.SingleOrDefault(x => x.PhoneNumber.Equals(phoneNumber));
            var result = VerifyUserStatus.VerifiedFaild;
            if (_u != null)
            {
                result = Verify(_u, password);
                if (result == VerifyUserStatus.VerifiedSuccessfully)
                {
                    correctUserName = _u.UserName;
                    userId = _u.id;
                    _u.IP = ip;
                    _u.RegisterType = UserRegisterType.Active;
                }
            }
            return result;
        }

        #endregion

        #region manager User

        public ChangePasswordResult ChangePasswordByUserName(String userName, String oldPassword, String newPassword)
        {
            throw new NotImplementedException();
        }
        public ChangePasswordResult ChangePasswordByUserId(Int64 Id, String oldPassword, String newPassword)
        {
            throw new NotImplementedException();
        }
        public void DeActiveUser(Int64 id)
        {
            throw new NotImplementedException();
        }

        public void ActiveUser(Int64 id)
        {
            throw new NotImplementedException();
        }
        public void DeActiveUsers(int[] usersId)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Int64 id) =>
            _user.Find(id);

        public User GetUserByUserName(String userName) =>
            _user.SingleOrDefault(x => x.UserName.Equals(userName));

        public User GetUserByPhoneNumber(String phoneNumber) =>
            _user.SingleOrDefault(x => x.PhoneNumber.Equals(phoneNumber));

        public IList<User> GetAllUsers() =>
            _user.AsNoTracking().ToList();

        public IList<User> GetUser(Expression<Func<User, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public Boolean IsUserActive(Int64 id) =>
            _user.Any(x => x.IsBaned);

        public Int32 GetUsersNumber()
        {
            throw new NotImplementedException();
        }

        public IList<String> SearchUserName(String userName)
        {
            throw new NotImplementedException();
        }

        public IList<String> SearchUserId(String userId)
        {
            throw new NotImplementedException();
        }
        public Role GetUserRole(Int64 userId)
        {
            throw new NotImplementedException();
        }
        public EditUserViewModel GetUserDataForEdit(Int64 userId) =>
            _user.Include(x => x.Role)
            .Where(x => x.id.Equals(userId))
            .Select(x => new EditUserViewModel()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
                Id = x.id,
                RoleId = x.Role.Id,
                IsBaned = x.IsBaned
            }).SingleOrDefault();

        public User Find(String userName) =>
            _user.SingleOrDefault(x => x.UserName.Equals(userName));

        public IList<String> GetUsersPhoneNumbers() =>
            _user.AsNoTracking().Select(x => x.PhoneNumber).ToList();

        public String GetRoleByUserName(String userName) =>
            _user.SingleOrDefault(x => x.UserName.Equals(userName)).Role.Name;

        public String GetRoleByPhoneNumber(String phoneNumber) =>
            _user.SingleOrDefault(x => x.PhoneNumber.Equals(phoneNumber)).Role.Name;

        public String GetUserNameByPhoneNumber(String phoneNumber) =>
            _user.SingleOrDefault(x => x.PhoneNumber.Equals(phoneNumber)).UserName;

        public IList<String> SearchByUserName(String userName)
        {
            throw new NotImplementedException();
        }

        public IList<String> SearchByRoleDescription(String roleDescription)
        {
            throw new NotImplementedException();
        }

        public IList<String> SearchByFirstName(String firstName)
        {
            throw new NotImplementedException();
        }

        public IList<String> SearchByLastName(String lastName)
        {
            throw new NotImplementedException();
        }

        public IList<String> SearchByPhoneNumber(String phoneNumber)
        {
            throw new NotImplementedException();
        }

        public IList<String> SearchByIP(String ip)
        {
            throw new NotImplementedException();
        }

        public EditedUserStatus UpdateProfile(ProfileViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Boolean Authenticate(String phoneNumber, String password)
        {
            throw new NotImplementedException();
        }

        public Boolean IsBaned(String userName)
        {
            throw new NotImplementedException();
        }

        public UserStatus GetStatus(String userName) =>
            _user.Where(x => x.UserName.Equals(userName))
            .Select(x => new UserStatus()
            {
                IsBaned = x.IsBaned,
                Role = x.Role.Name
            }).SingleOrDefault();
        public IList<UserIdAndUserName> SearchUser(String userName)
        {
            throw new NotImplementedException();
        }
        public ProfileViewModel GetUserDataForUpdateProfile(Int64 userId)
        {
            throw new NotImplementedException();
        }
        public void Remove(Int64 id)
        {
            _user.Include(x => x.Role).Where(x => x.id.Equals(id) && x.Role.Name != "admin").Delete();
        }
        public Boolean LimitAddToWishList(String userName) =>
            GetUserByUserName(userName).ProductsFavorite.Count > 50;
        public IEnumerable<ProductSectionViewModel> GetUserWishList(out Int32 total,
                                                                                                                    Int32 page,
                                                                                                                    Int32 count,
                                                                                                                    String userName)
        {
            User _model = _user.AsNoTracking().Include(x => x.ProductsFavorite).SingleOrDefault(x => x.UserName.Equals(userName));
            total = 0;
            if (_model == null)
                return null;
            total = _model.ProductsFavorite.Count;
            IQueryable<ProductSectionViewModel> prod = _model.ProductsFavorite.AsQueryable()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * count)
                .Take(count)
                .Select(x => new ProductSectionViewModel()
                {
                    AvrageRate = x.Rate.AverageRating,
                    Id = x.Id,
                    IsFreeShipping = x.IsFreeShipping,
                    IsInStock = x.Stock - x.Reserve >= x.Ratio,
                    Name = x.Name,
                    Price = x.Price,
                    Ratio = x.Ratio,
                    PrincipleImagePath = x.PrincipleImagePath,
                    SellCount = x.SellCount,
                    TotalDiscount = x.DiscountPercent + x.Category.DiscountPercent,
                    ViewCount = x.ViewCount
                });
            return prod.ToList();
        }

        public void RemoveUserComments(Int64 userId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
