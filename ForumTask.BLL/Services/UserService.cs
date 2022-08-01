using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;

namespace ForumTask.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityManager identityManager;

        public UserService(IIdentityManager identityManager)
        {
            this.identityManager = identityManager;
        }

        public async Task DeleteAsync(long userId, long callerId)
        {
            var user = await identityManager.FindAsync(userId) ?? throw new NotFoundException();

            if (userId != callerId)
            {
                await CheckRightAsync(user, callerId);
            }

            await identityManager.DeleteAsync(user);
        }

        public async Task<UserDTO> GetAsync(long userId)
        {
            var user = await identityManager.FindAsync(userId) ?? throw new NotFoundException();

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
                await identityManager.CreateAsync(userName, email, password);
            }
            catch (Identity.IdentityException exception)
            {
                throw new IdentityValidationException(exception);
            }
        }

        public async Task SetBannedAsync(long userId, bool banned, long callerId)
        {
            var user = await identityManager.FindAsync(userId) ?? throw new NotFoundException();

            await CheckRightAsync(user, callerId);

            if (user.IsBanned != banned)
            {
                user.IsBanned = banned;

                await identityManager.UpdateAsync(user);
            }
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

            var user = await identityManager.FindAsync(userId) ?? throw new NotFoundException();

            await CheckRightAsync(user, callerId);

            try
            {
                if (setHasRole)
                {
                    await identityManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    await identityManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            catch (Identity.IdentityException) { }
        }

        public async Task SignInAsync(string userName, string password, bool remember)
        {
            var signedIn = await identityManager.TrySignInAsync(userName, password, remember);

            if (!signedIn)
            {
                throw new AccessDeniedException("Wrong user name or password");
            }
        }

        public Task SignOutAsync()
        {
            return identityManager.SignOutAsync();
        }

        public Task<bool> IsEmailUsedAsync(string email)
        {
            return identityManager.IsEmailUsedAsync(email);
        }

        public Task<bool> IsUserNameUsedAsync(string userName)
        {
            return identityManager.IsUserNameUsedAsync(userName);
        }


        private async Task<RoleEnum> GetMaxRoleAsync(DAL.Entities.User user)
        {
            var roles = await identityManager.GetRolesAsync(user);
            return roles.Select(s => RoleEnumConverter.GetRoleByName(s)).Max();
        }

        private async Task CheckRightAsync(DAL.Entities.User victim, long callerId)
        {
            var caller = await GetAsync(callerId);
            var victimMaxRole = await GetMaxRoleAsync(victim);

            if (caller.MaxRole <= victimMaxRole)
            {
                throw new AccessDeniedException("To edit/delete this user your right-level must be greater then of that user");
            }
        }
    }
}
