using System.Data;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace ForumTask.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task DeleteAsync(long userId, long callerId)
        {
            var user = await FindUserById(userId);

            if (userId != callerId)
            {
                await CheckRightAsync(user, callerId);
            }

            var result = await userManager.DeleteAsync(user);
            AssertSucceeded(result);
        }

        public async Task<UserDTO> GetAsync(long userId)
        {
            var user = await FindUserById(userId);

            var userDto = new UserDTO(user)
            {
                MaxRole = await GetMaxRoleAsync(user)
            };

            return userDto;
        }

        public async Task RegisterAsync(string userName, string email, string password)
        {
            try
            {
                var user = new User
                {
                    UserName = userName,
                    Email = email,
                    RegisterDate = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user, password);
                AssertSucceeded(result);

                await userManager.AddToRoleAsync(user, "User");
            }
            catch (IdentityException exception)
            {
                throw new IdentityValidationException(exception);
            }
        }

        public async Task SetBannedAsync(long userId, bool banned, long callerId)
        {
            var user = await FindUserById(userId);

            await CheckRightAsync(user, callerId);

            if (user.IsBanned != banned)
            {
                user.IsBanned = banned;

                var result = await userManager.UpdateAsync(user);
                AssertSucceeded(result);
            }
        }

        private async Task<User> FindUserById(long userId)
        {
            var user = await FindUserById(userId);

            return user;
        }

        public async Task SetRoleAsync(long userId, string roleName, bool setHasRole, long callerId)
        {
            if (roleName.ToLower() == "user")
            {
                throw new InvalidOperationException();
            }

            var caller = await GetAsync(callerId);

            if (caller.MaxRole <= RoleEnumConverter.GetRoleByName(roleName))
            {
                throw new AccessDeniedException("Cant manage higher or equal roles");
            }

            var user = await FindUserById(userId);

            await CheckRightAsync(user, callerId);

            //Ignoring result check is normal
            await (setHasRole ? userManager.AddToRoleAsync(user, roleName) : userManager.RemoveFromRoleAsync(user, roleName));
        }

        public async Task SignInAsync(string userName, string password, bool remember)
        {
            var signedIn = await signInManager.PasswordSignInAsync(userName, password, remember, false).ContinueWith(x => x.Result.Succeeded);

            if (!signedIn)
            {
                throw new AccessDeniedException("Wrong user name or password");
            }
        }

        public Task SignOutAsync()
        {
            return signInManager.SignOutAsync();
        }

        public async Task<bool> IsEmailUsedAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> IsUserNameUsedAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            return user != null;
        }

        private async Task<RoleEnum> GetMaxRoleAsync(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return roles.Select(s => RoleEnumConverter.GetRoleByName(s)).Max();
        }

        private async Task CheckRightAsync(User victim, long callerId)
        {
            var caller = await GetAsync(callerId);
            var victimMaxRole = await GetMaxRoleAsync(victim);

            if (caller.MaxRole <= victimMaxRole)
            {
                throw new AccessDeniedException("To edit/delete this user your right-level must be greater then of that user");
            }
        }

        private static void AssertSucceeded(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new IdentityException(identityResult.Errors.Select(e => e.Code));
            }
        }
    }
}
