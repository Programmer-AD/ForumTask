using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumTask.PL.Filters;
using ForumTask.PL.Extensions;
using ForumTask.BLL.Interfaces;
using ForumTask.PL.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace ForumTask.PL.Controllers {
    [Route("api/user")]
    [ApiController]
    [ModelValidFilter]
    [BllExceptionFilter]
    public class UserController : ControllerBase {
        private readonly IUserService userServ;

        public UserController(IUserService userService) {
            userServ = userService;
        }

        [HttpGet("{userId}")]
        public UserViewModel Get(int userId)
            => new (userServ.Get(userId));

        [HttpGet("canUse/email/{email}")]
        public bool CanUseEmail([Required] string email)
            => !userServ.IsEmailUsed(email);

        [HttpGet("canUse/userName/{userName}")]
        public bool CanUseUserName([Required] string userName)
            => !userServ.IsUserNameUsed(userName);

        [HttpPost("register")]
        public IActionResult Register(RegisterModel register) {
            userServ.Register(register.UserName, register.Email, register.Password);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId) {
            userServ.Delete(userId, User.GetId());
            return Ok();
        }

        [Authorize]
        [HttpPut("{userId}/banned")]
        public IActionResult SetBanned(int userId, bool banned) {
            userServ.SetBanned(userId, banned, User.GetId());
            return Ok();
        }

        [Authorize]
        [HttpPut("{userId}/roles")]
        public IActionResult SetRole(int userId, [Required] string roleName, bool setHasRole) {
            userServ.SetRole(userId, roleName, setHasRole, User.GetId());
            return Ok();
        }

        [HttpPost("signIn")]
        public IActionResult SignIn(SignInModel model) {
            userServ.SignIn(model.UserName, model.Password, model.Remember);
            return Ok();
        }

        [Authorize]
        [HttpPost("signOut")]
        public new IActionResult SignOut() {
            userServ.SignOut();
            return Ok();
        }

        [Authorize]
        [HttpGet("current")]
        public UserViewModel GetCurrentUser()
            => new(userServ.Get(User.GetId()));
        
    }
}
