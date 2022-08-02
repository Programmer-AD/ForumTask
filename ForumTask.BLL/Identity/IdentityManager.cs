using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace ForumTask.BLL.Identity
{
    internal class IdentityManager : IIdentityManager
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public IdentityManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public Task AddToRoleAsync(User user, string role)
        {
            return userManager.AddToRoleAsync(user, role);
        }

        public async Task CreateAsync(string userName, string email, string password)
        {
            var user = new User
            {
                UserName = userName,
                Email = email,
                RegisterDate = DateTime.UtcNow
            };

            await userManager.CreateAsync(user, password);

            await AddToRoleAsync(user, "User");
        }

        public Task DeleteAsync(User user)
        {
            return userManager.DeleteAsync(user);
        }

        public Task<User> FindAsync(long id)
        {
            return userManager.FindByIdAsync(id.ToString());
        }

        public Task RemoveFromRoleAsync(User user, string role)
        {
            return userManager.RemoveFromRoleAsync(user, role);
        }

        public Task<bool> TrySignInAsync(string userName, string password, bool remember)
        {
            return signInManager.PasswordSignInAsync(userName, password, remember, false).ContinueWith(x => x.Result.Succeeded);
        }

        public Task SignOutAsync()
        {
            return signInManager.SignOutAsync();
        }

        public Task UpdateAsync(User user)
        {
            return userManager.UpdateAsync(user);
        }

        public Task<IEnumerable<string>> GetRolesAsync(User user)
        {
            return userManager.GetRolesAsync(user).ContinueWith(x => (IEnumerable<string>)x.Result);
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

        private static void AssertSucceeded(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new IdentityException(identityResult.Errors.Select(e => e.Code));
            }
        }
    }
}
