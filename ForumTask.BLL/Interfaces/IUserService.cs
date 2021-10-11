using ForumTask.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumTask.BLL.Exceptions;

namespace ForumTask.BLL.Interfaces {
    public interface IUserService {
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
        void Register(string userName, string email, string password);
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
        void SetBanned(uint userId, bool banned, uint callerId);
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
        void Delete(uint userId, uint callerId);
        /// <summary>
        /// Gets user by id
        /// <para>
        /// If user not found, throw <see cref="NotFoundException"/>
        /// </para>
        /// </summary>
        /// <param name="userId">Id of user to get</param>
        /// <returns>User with id=<paramref name="userId"/></returns>
        /// <exception cref="NotFoundException"/>
        UserDTO Get(uint userId);
        /// <summary>
        /// Attach/detach role to/from user depending on <paramref name="setHasRole"/>
        /// <para>
        /// If user not found, throw <see cref="NotFoundException"/>
        /// </para>
        /// <para>
        /// If role can`t be attached/detached, throw <see cref="AccessDeniedException"/>
        /// </para>
        /// </summary>
        /// <param name="userId">Id of user to act with</param>
        /// <param name="roleName">Name of role to act with</param>
        /// <param name="callerId">Id of user who tries to set role</param>
        /// <param name="setHasRole">If true than attaches role, else detach role</param>
        /// <exception cref="NotFoundException"/>
        /// <exception cref="AccessDeniedException"/>
        void SetRole(uint userId, string roleName, bool setHasRole, uint callerId);
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
        void SignIn(string userName, string password, bool remember);
        /// <summary>
        /// Sign out current user
        /// </summary>
        void SignOut();
        /// <summary>
        /// Checks if email address is used
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>Is email used?</returns>
        bool IsEmailUsed(string email);
        /// <summary>
        /// Checks if user name is used
        /// </summary>
        /// <param name="userName">Role name to check</param>
        /// <returns>Is role name used?</returns>
        bool IsUserNameUsed(string userName);
    }
}
