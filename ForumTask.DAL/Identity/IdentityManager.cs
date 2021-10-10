using ForumTask.DAL.Entities;
using ForumTask.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.Identity {
    class IdentityManager:IIdentityManager {
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
                throw new IdentityException(t.Result.Errors.Select(e=>e.Code));
        }

        public void AddToRole(User user, string role) {
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            CallIdentitySync(() => userMan.AddToRoleAsync(user, role));
        }

        public void Create(string userName, string email, string password) {
            CallIdentitySync(()=>userMan.CreateAsync(new User {
                UserName = userName,
                Email = email,
                RegisterDate = DateTime.UtcNow
            }, password));
            
        }

        public void Delete(User user) {
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            CallIdentitySync(() => userMan.DeleteAsync(user));
        }

        public void Delete(uint id) {
            User user = FindById(id)??
                throw new InvalidOperationException("User with providden id wasn`t found, so can`t be deleted");
            Delete(user);
        }

        public User FindById(uint id) {
            var t=userMan.FindByIdAsync(id.ToString());
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
    }
}
