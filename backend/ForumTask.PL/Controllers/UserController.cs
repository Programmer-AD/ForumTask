using System.ComponentModel.DataAnnotations;
using ForumTask.BLL.Interfaces;
using ForumTask.PL.Extensions;
using ForumTask.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumTask.PL.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<UserViewModel> GetAsync(long userId)
        {
            var userDto = await userService.GetAsync(userId);

            var userViewModel = new UserViewModel(userDto);

            return userViewModel;
        }

        [HttpGet("canUse/email/{email}")]
        public Task<bool> CanUseEmailAsync([Required] string email)
        {
            return userService.IsEmailUsedAsync(email).ContinueWith(x => !x.Result);
        }

        [HttpGet("canUse/userName/{userName}")]
        public Task<bool> CanUseUserNameAsync([Required] string userName)
        {
            return userService.IsUserNameUsedAsync(userName).ContinueWith(x => !x.Result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel register)
        {
            await userService.RegisterAsync(register.UserName, register.Email, register.Password);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(long userId)
        {
            await userService.DeleteAsync(userId, User.GetId());

            return Ok();
        }

        [Authorize]
        [HttpPut("{userId}/banned/{banned}")]
        public async Task<IActionResult> SetBannedAsync(long userId, bool banned)
        {
            await userService.SetBannedAsync(userId, banned, User.GetId());

            return Ok();
        }

        [Authorize]
        [HttpPut("{userId}/roles")]
        public async Task<IActionResult> SetRoleAsync(long userId, UserRoleSetModel model)
        {
            await userService.SetRoleAsync(userId, model.RoleName, model.SetHasRole, User.GetId());

            return Ok();
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignInAsync(SignInModel model)
        {
            await userService.SignInAsync(model.UserName, model.Password, model.Remember);

            return Ok();
        }

        [Authorize]
        [HttpPost("signOut")]
        public async Task<IActionResult> SignOutAsync()
        {
            await userService.SignOutAsync();

            return Ok();
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<UserViewModel> GetCurrentUserAsync()
        {
            var userDto = await userService.GetAsync(User.GetId());

            var userViewModel = new UserViewModel(userDto);

            return userViewModel;
        }
    }
}
