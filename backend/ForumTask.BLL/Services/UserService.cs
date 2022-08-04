using System.Data;
using AutoMapper;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL;
using ForumTask.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace ForumTask.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(long userId, long callerId)
        {
            var user = await FindUserByIdAsync(userId);

            if (userId != callerId)
            {
                await CheckRightAsync(user, callerId);
            }

            var result = await userManager.DeleteAsync(user);
            AssertSucceeded(result);
        }

        public async Task<UserDto> GetAsync(long userId)
        {
            var user = await FindUserByIdAsync(userId);

            var userDto = mapper.Map<UserDto>(user);
            userDto.MaxRole = await GetMaxRoleAsync(user);

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

                result = await userManager.AddToRoleAsync(user, RoleNames.User);
                AssertSucceeded(result);
            }
            catch (IdentityException exception)
            {
                throw new IdentityValidationException(exception);
            }
        }

        public async Task SetBannedAsync(long userId, bool banned, long callerId)
        {
            var user = await FindUserByIdAsync(userId);

            await CheckRightAsync(user, callerId);

            if (user.IsBanned != banned)
            {
                user.IsBanned = banned;

                var result = await userManager.UpdateAsync(user);
                AssertSucceeded(result);
            }
        }

        public async Task SetRoleAsync(long userId, string roleName, bool setHasRole, long callerId)
        {
            var isCorrectRoleName = Enum.TryParse<RoleEnum>(roleName, out var role);

            if (!isCorrectRoleName || role == RoleEnum.User)
            {
                throw new InvalidOperationException();
            }

            var caller = await GetAsync(callerId);

            if (caller.MaxRole <= role)
            {
                throw new AccessDeniedException("Cant manage higher or equal roles");
            }

            var user = await FindUserByIdAsync(userId);

            await CheckRightAsync(user, callerId);

            //Ignoring result check is normal
            await (setHasRole ? userManager.AddToRoleAsync(user, roleName) : userManager.RemoveFromRoleAsync(user, roleName));
        }

        public async Task SignInAsync(string userName, string password, bool remember)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password, remember, false);

            var signedIn = result.Succeeded;

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

        private Task<User> FindUserByIdAsync(long userId)
        {
            return userManager.FindByIdAsync(userId.ToString());
        }

        private async Task<RoleEnum> GetMaxRoleAsync(User user)
        {
            var roles = await userManager.GetRolesAsync(user);

            var result = roles.Select(x => Enum.Parse<RoleEnum>(x)).Max();

            return result;
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
