using System;
using System.Collections.Generic;
using System.Linq;
using ForumTask.BLL.DTO;
using ForumTask.BLL.Identity;
using ForumTask.BLL.Interfaces;
using ForumTask.DAL.Entities;

namespace ForumTask.Tests.Fakes
{
    internal class UserWithRoles
    {
        public User User { get; set; }
        public HashSet<RoleEnum> Roles { get; set; } = new HashSet<RoleEnum>();
    }

    internal class FakeIdentityManager : IIdentityManager
    {
        private readonly List<UserWithRoles> data;

        public FakeIdentityManager(params UserWithRoles[] data)
        {
            this.data = data.ToList();
        }

        public void AddToRole(User user, string role)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            RoleEnum re;
            try
            {
                re = RoleEnumConverter.GetRoleByName(role);
            }
            catch (ArgumentException)
            {
                throw new IdentityException("No such role");
            }
            var us = data.Find(u => u.User.Id == user.Id);
            us.Roles.Add(re);
        }

        public void Create(string userName, string email, string password)
        {
            if (IsEmailUsedAsync(email) || IsUserNameUsedAsync(userName) || password.Length < 6)
            {
                throw new IdentityException("Wrong regitration data");
            }

            data.Add(new()
            {
                User = new User(userName.ToLower())
                {
                    Id = data.Count == 0 ? 1 : data.Max(u => u.User.Id) + 1,
                    Email = email.ToLower(),
                    PasswordHash = password
                }
            });
        }

        public void Delete(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            int ind = data.FindIndex(u => u.User.Id == user.Id);
            if (ind >= 0)
            {
                data.RemoveAt(ind);
            }
        }

        public void Delete(int id)
        {
            int ind = data.FindIndex(u => u.User.Id == id);
            if (ind >= 0)
            {
                data.RemoveAt(ind);
            }
            else
            {
                throw new InvalidOperationException("No user with such id");
            }
        }

        public User FindById(int id)
        {
            return data.Find(u => u.User.Id == id)?.User;
        }

        public IList<string> GetRoles(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return data.Find(u => u.User.Id == user.Id).Roles
                .Select(r => r.GetRoleName()).ToList();
        }

        public bool IsEmailUsedAsync(string email)
        {
            return data.Find(u => u.User.Email == email.ToLower()) is not null;
        }

        public bool IsUserNameUsedAsync(string userName)
        {
            return data.Find(u => u.User.UserName == userName.ToLower()) is not null;
        }

        public void RemoveFromRole(User user, string role)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            RoleEnum re;
            try
            {
                re = RoleEnumConverter.GetRoleByName(role);
            }
            catch (ArgumentException)
            {
                throw new IdentityException("No such role");
            }
            var us = data.Find(u => u.User.Id == user.Id);
            us.Roles.Remove(re);
        }

        public bool SignedOut { get; private set; }
        public void SignOut()
        {
            SignedOut = true;
        }

        public bool TrySignInAsync(string userName, string password, bool remember)
        {
            return data.Find(u => u.User.UserName == userName.ToLower())?.User.PasswordHash == password;
        }

        public void Update(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var res = data.Find(u => u.User.Id == user.Id);
            res.User = user;
        }
    }
}
