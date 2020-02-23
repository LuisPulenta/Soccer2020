using Microsoft.AspNetCore.Identity;
using Soccer2020.Web.Data.Entities;
using Soccer2020.Web.Models;
using System.Threading.Tasks;

namespace Soccer2020.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        
        Task<SignInResult> LoginAsync(LoginViewModel model);
        
        Task LogoutAsync();

    }
}