using ForumTask.DAL.EF.Repositories;
using ForumTask.DAL.Entities;
using ForumTask.DAL.Identity;
using ForumTask.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumTask.DAL.EF {
    public class UnitOfWork : IUnitOfWork {
        private ITopicRepository topic;
        public ITopicRepository Topics => topic ??= new TopicRepository(db);

        private IMessageRepository message;
        public IMessageRepository Messages => message ??= new MessageRepository(db);

        private IMarkRepository mark;
        public IMarkRepository Marks => mark ??= new MarkRepository(db);

        private IIdentityManager identity;
        public IIdentityManager IdentityManager => identity ??= new IdentityManager(userManager,signInManager);

        private readonly ForumContext db;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UnitOfWork(ForumContext context, UserManager<User> userManager, SignInManager<User> signInManager) {
            db = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void SaveChanges()
            => db.SaveChanges();
    }
}
