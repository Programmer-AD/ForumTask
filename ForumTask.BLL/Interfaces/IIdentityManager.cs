using System;
using System.Collections.Generic;
using ForumTask.BLL.Identity;
using ForumTask.DAL.Entities;

namespace ForumTask.BLL.Interfaces
{
    public interface IIdentityManager
    {
        /// <summary>
        /// Creates new user using data from arguments
        /// </summary>
        /// <param name="userName">Name of new user</param>
        /// <param name="email">Email of new user</param>
        /// <param name="password">Password of new user</param>
        /// <exception cref="IdentityException"/>
        void Create(string userName, string email, string password);
        /// <summary>
        /// Finds user with providden Id
        /// </summary>
        /// <param name="id">id of user to find</param>
        /// <returns>User or null if not found</returns>
        User FindById(int id);
        /// <summary>
        /// Updates user
        /// <para>
        /// If <paramref name="user"/> is null, throws <see cref="ArgumentNullException"/>
        /// </para>
        /// </summary>
        /// <param name="user">User to update</param>
        /// <exception cref="IdentityException"/>
        /// <exception cref="ArgumentNullException"/>
        void Update(User user);
        /// <summary>
        /// Deletes user
        /// <para>
        /// If <paramref name="user"/> is null, throws <see cref="ArgumentNullException"/>
        /// </para>
        /// </summary>
        /// <param name="user">User to delete</param>
        /// <exception cref="ArgumentNullException"/>
        void Delete(User user);
        /// <summary>
        /// Updates user
        /// <para>
        /// If user with <paramref name="id"/> not foun, throws <see cref="InvalidOperationException"/>
        /// </para>
        /// </summary>
        /// <param name="id">Id of user to delete</param>
        /// <exception cref="InvalidOperationException"/>
        void Delete(int id);
        /// <summary>
        /// Adds role with name <paramref name="role"/> to <paramref name="user"/>
        /// <para>
        /// If <paramref name="user"/> is null, throws <see cref="ArgumentNullException"/>
        /// </para>
        /// <para>
        /// If there is no role with name <paramref name="role"/>, throws <see cref="IdentityException"/>
        /// </para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IdentityException"/>
        void AddToRole(User user, string role);
        /// <summary>
        /// Removes role with name <paramref name="role"/> from <paramref name="user"/>
        /// <para>
        /// If <paramref name="user"/> is null, throws <see cref="ArgumentNullException"/>
        /// </para>
        /// <para>
        /// If there is no role with name <paramref name="role"/>, throws <see cref="IdentityException"/>
        /// </para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IdentityException"/>
        void RemoveFromRole(User user, string role);
        /// <summary>
        /// Tries to sign in using <paramref name="userName"/> and <paramref name="password"/>
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <param name="remember">Save cookie after browser closed?</param>
        /// <returns>Is signed in?</returns>
        bool TrySignIn(string userName, string password, bool remember);
        /// <summary>
        /// Sign out of current account
        /// </summary>
        void SignOut();
        /// <summary>
        /// Get list of role name attached to <paramref name="user"/>
        /// <para>
        /// If <paramref name="user"/> is null, throw <see cref="ArgumentNullException"/>
        /// </para>
        /// </summary>
        /// <param name="user">User whose roles must be getted</param>
        /// <returns>List of role names</returns>
        /// <exception cref="ArgumentNullException"/>
        IList<string> GetRoles(User user);
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
