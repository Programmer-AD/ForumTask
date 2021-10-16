using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumTask.BLL.Identity {
    internal class IdentityManager : IIdentityManager, IDisposable {
        private readonly UserManager<User> userMan;
        private readonly SignInManager<User> signInMan;

        public IdentityManager(UserManager<User> userManager, SignInManager<User> signInManager) {
            userMan = userManager;
            signInMan = signInManager;
        }

        private static void CallIdentitySync(Func<Task<IdentityResult>> func) {
            var t = func();
            t.Wait();
            if (!t.Result.Succeeded)
                throw new IdentityException(t.Result.Errors.Select(e => e.Code));
        }

        public void AddToRole(User user, string role) {
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            CallIdentitySync(() => userMan.AddToRoleAsync(user, role));
        }

        public void Create(string userName, string email, string password) {
            var user = new User {
                UserName = userName,
                Email = email,
                RegisterDate = DateTime.UtcNow
            };
            CallIdentitySync(() => userMan.CreateAsync(user, password));
            AddToRole(user, "User");
        }

        public void Delete(User user) {
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            CallIdentitySync(() => userMan.DeleteAsync(user));
        }

        public void Delete(int id) {
            User user = FindById(id) ??
                throw new InvalidOperationException("User with providden id wasn`t found, so can`t be deleted");
            Delete(user);
        }

        public User FindById(int id) {
            var t = userMan.FindByIdAsync(id.ToString());
            t.Wait();
            return t.Result;
        }

        public User FindByName(string name) {
            var t = userMan.FindByNameAsync(name);
            t.Wait();
            return t.Result;
        }

        public void RemoveFromRole(User user, string role) {
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            CallIdentitySync(() => userMan.RemoveFromRoleAsync(user, role));
        }

        public bool TrySignIn(string userName, string password, bool remember) {
            var t = signInMan.PasswordSignInAsync(userName, password, remember, false);
            t.Wait();
            return t.Result.Succeeded;
        }

        public void SignOut() {
            signInMan.SignOutAsync().Wait();
        }

        public void Update(User user) {
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            CallIdentitySync(() => userMan.UpdateAsync(user));
        }

        public IList<string> GetRoles(User user) {
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            var t = userMan.GetRolesAsync(user);
            t.Wait();
            return t.Result;
        }

        public bool IsEmailUsed(string email) {
            var t = userMan.FindByEmailAsync(email);
            t.Wait();
            return t.Result is not null;
        }

        public bool IsUserNameUsed(string userName) {
            var t = userMan.FindByNameAsync(userName);
            t.Wait();
            return t.Result is not null;
        }

        public void Dispose() {
            userMan.Dispose();
        }
    }
}
