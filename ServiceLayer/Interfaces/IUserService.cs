using DomainClasses.Entities;
using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Product;
using ViewModel.ViewModel.Admin.User;

namespace ServiceLayer.Interfaces
{
    public class UserIdAndUserName
    {
        public Int32 Id { get; set; }
        public String UserName { get; set; }
    }

    public class UserStatus
    {
        public Boolean IsBaned { get; set; }
        public String Role { get; set; }
    }

    public interface IUserService
    {
        String GeneratePassword();
        Task<AddUserStatus> AddAsync(User user);
        Task RemoveAsync(Int64 id);
        void RemoveUserComments(Int64 userId);
        VerifyUserStatus VerifyUserByPhoneNumber(String phoneNumber, String password, ref String correctUserName,
            ref Int64 userId, String ip);
        ChangePasswordResult ChangePasswordByUserName(String userName, String oldPassword, String newPassword);
        ChangePasswordResult ChangePasswordByUserId(Int64 Id, String oldPassword, String newPassword);
        void DeActiveUser(Int64 id);
        void DeActiveUsers(Int32[] usersId);
        void ActiveUser(Int64 id);
        Task<User> GetUserByIdAsync(Int64 id);
        Task<User> GetUserByUserName(String userName);
        Task<User> GetUserByPhoneNumber(String phoneNumber);
        IList<User> GetAllUsers();
        IList<User> GetUser(Expression<Func<User, Boolean>> expression);
        Task<Boolean> ExistsByUserNameAsync(String userName);
        Task<Boolean> ExistsByPhoneNumberAsync(String phoneNumber);
        Task<Boolean> ExistsByUserNameAsync(String userName, Int64 id);
        Task<Boolean> ExistsByPhoneNumberAsync(String phoneNumber, Int64 id);
        Boolean IsUserActive(Int64 id);

        Task<DetailsUserViewModel> GetUserDetailAsync(Int64 id);
        Int32 GetUsersNumber();
        IList<String> SearchUserName(String userName);
        IList<String> SearchUserId(String userId);
        Role GetUserRole(Int64 userId);
        Task<EditUserViewModel> GetUserDataForEditAsync(Int64 userId);
        ProfileViewModel GetUserDataForUpdateProfile(Int64 userId);
        Task<EditedUserStatus> EditUserAsync(User user);

        IList<UserViewModel> GetDataTable(out Int32 total, String term, Int32 page, Int32 count,
            DomainClasses.Enums.Order order, UserOrderBy orderBy, UserSearchBy searchBy);

        User Find(String userName);

        IList<String> GetUsersPhoneNumbers();

        String GetRoleByUserName(String userName);

        String GetRoleByPhoneNumber(String phoneNumber);
        String GetUserNameByPhoneNumber(String phoneNumber);

        IList<String> SearchByUserName(String userName);
        IList<String> SearchByRoleDescription(String roleDescription);
        IList<String> SearchByFirstName(String firstName);
        IList<String> SearchByLastName(String lastName);
        IList<String> SearchByPhoneNumber(String phoneNumber);
        IList<String> SearchByIP(String ip);

        IEnumerable<ProductSectionViewModel> GetUserWishList(out Int32 total, Int32 page, Int32 count, String userName);
        EditedUserStatus UpdateProfile(ProfileViewModel viewModel);
        Task<EditedUserStatus> UpdateInfoOrder(UserWithAddressViewModel viewModel, String userName);
        Boolean Authenticate(String phoneNumber, String password);
        Boolean IsBaned(String userName);
        UserStatus GetStatus(String userName);
        IList<UserIdAndUserName> SearchUser(String userName);
        Task<Boolean> LimitAddToWishList(String userName);
        Task<User> GetUserByUserNameIncludeAddress(String username, Boolean AsNoTracking);
        Task<UserWithAddressViewModel> GetInfoUserForOrder(String username);
    }
}
