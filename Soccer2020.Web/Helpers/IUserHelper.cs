using Microsoft.AspNetCore.Identity;
using Soccer2020.Common.Enums;
using Soccer2020.Web.Data.Entities;
using Soccer2020.Web.Models;
using System;
using System.Threading.Tasks;

namespace Soccer2020.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserAsync(string email);

        Task<UserEntity> GetUserAsync(Guid userId);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        
        Task<SignInResult> LoginAsync(LoginViewModel model);
        
        Task LogoutAsync();

        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path, UserType userType);

        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);

    }
}