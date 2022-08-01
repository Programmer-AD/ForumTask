using ForumTask.BLL.DTO;
using ForumTask.BLL.Exceptions;

namespace ForumTask.BLL.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Register new user
        /// <para>
        /// If provided wrong registration data, throw <see cref="IdentityValidationException"/>
        /// </para>
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <exception cref="IdentityValidationException"/>
        Task RegisterAsync(string userName, string email, string password);

        /// <summary>
        /// Changes ban status of user with id=<paramref name="userId"/>
        /// <para>
        /// If user not found, throw <see cref="NotFoundException"/>
        /// </para>
        /// <para>
        /// If user can`t be banned/unbanned, throw <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="userId">User whose ban status is changed</param>
        /// <param name="banned">Value of ban</param>
        /// <param name="callerId">User who tries to change ban status</param>
        /// <exception cref="NotFoundException"/>
        /// <exception cref="AccessDeniedException"/>
        Task SetBannedAsync(long userId, bool banned, long callerId);

        /// <summary>
        /// Deletes user with id=<paramref name="userId"/>
        /// <para>
        /// If user not found, throw <see cref="NotFoundException"/>
        /// </para>
        /// <para>
        /// If user can`t be deleted, throw <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="userId">User to delete</param>
        /// <param name="callerId">User who tries to delete</param>
        /// <exception cref="NotFoundException"/>
        /// <exception cref="AccessDeniedException"/>
        Task DeleteAsync(long userId, long callerId);

        /// <summary>
        /// Gets user by id
        /// <para>
        /// If user not found, throw <see cref="NotFoundException"/>
        /// </para>
        /// </summary>
        /// <param name="userId">Id of user to get</param>
        /// <returns>User with id=<paramref name="userId"/></returns>
        /// <exception cref="NotFoundException"/>
        Task<UserDTO> GetAsync(long userId);

        /// <summary>
        /// Attach/detach role to/from user depending on <paramref name="setHasRole"/>
        /// <para>
        /// If user not found, throw <see cref="NotFoundException"/>
        /// </para>
        /// <para>
        /// If role can`t be attached/detached, throw <see cref="AccessDeniedException"/>
        /// </para>
        /// <para>
        /// If trying to set/remove base role "User", throw <see cref="InvalidOperationException"/>
        /// </para>
        /// </summary>
        /// <param name="userId">Id of user to act with</param>
        /// <param name="roleName">Name of role to act with</param>
        /// <param name="callerId">Id of user who tries to set role</param>
        /// <param name="setHasRole">If true than attaches role, else detach role</param>
        /// <exception cref="NotFoundException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="AccessDeniedException"/>
        Task SetRoleAsync(long userId, string roleName, bool setHasRole, long callerId);

        /// <summary>
        /// Sign in user
        /// <para>
        /// If can`t sign in, throws <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="userName">User name of account to sign in</param>
        /// <param name="password">Password in text form</param>
        /// <param name="remember">Save cookie after browser closed?</param>
        /// <exception cref="AccessDeniedException"/>
        Task SignInAsync(string userName, string password, bool remember);

        /// <summary>
        /// Sign out current user
        /// </summary>
        Task SignOutAsync();

        /// <summary>
        /// Checks if email address is used
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>Is email used?</returns>
        Task<bool> IsEmailUsedAsync(string email);

        /// <summary>
        /// Checks if user name is used
        /// </summary>
        /// <param name="userName">Role name to check</param>
        /// <returns>Is role name used?</returns>
        Task<bool> IsUserNameUsedAsync(string userName);
    }
}
