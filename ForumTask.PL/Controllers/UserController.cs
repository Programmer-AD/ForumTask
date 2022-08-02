using System.ComponentModel.DataAnnotations;
using ForumTask.BLL.Interfaces;
using ForumTask.PL.Extensions;
using ForumTask.PL.Filters;
using ForumTask.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumTask.PL.Controllers
{
    [Route("api/user")]
    [ApiController]
    [ModelValidFilter]
    [BllExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService userServ;

        public UserController(IUserService userService)
        {
            userServ = userService;
        }

        [HttpGet("{userId}")]
        public UserViewModel Get(int userId)
        {
            return new(userServ.GetAsync(userId));
        }

        [HttpGet("canUse/email/{email}")]
        public bool CanUseEmail([Required] string email)
        {
            return !userServ.IsEmailUsedAsync(email);
        }

        [HttpGet("canUse/userName/{userName}")]
        public bool CanUseUserName([Required] string userName)
        {
            return !userServ.IsUserNameUsedAsync(userName);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel register)
        {
            userServ.RegisterAsync(register.UserName, register.Email, register.Password);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            userServ.DeleteAsync(userId, User.GetId());
            return Ok();
        }

        [Authorize]
        [HttpPut("{userId}/banned/{banned}")]
        public IActionResult SetBanned(int userId, bool banned)
        {
            userServ.SetBannedAsync(userId, banned, User.GetId());
            return Ok();
        }

        [Authorize]
        [HttpPut("{userId}/roles")]
        public IActionResult SetRole(int userId, UserRoleSetModel model)
        {
            userServ.SetRoleAsync(userId, model.RoleName, model.SetHasRole, User.GetId());
            return Ok();
        }

        [HttpPost("signIn")]
        public IActionResult SignIn(SignInModel model)
        {
            userServ.SignInAsync(model.UserName, model.Password, model.Remember);
            return Ok();
        }

        [Authorize]
        [HttpPost("signOut")]
        public new IActionResult SignOut()
        {
            userServ.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("current")]
        public UserViewModel GetCurrentUser()
        {
            return new(userServ.GetAsync(User.GetId()));
        }
    }
}
