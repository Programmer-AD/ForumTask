using ForumTask.BLL.DTO;
using ForumTask.BLL.Interfaces;
using ForumTask.PL.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumTask.PL.Controllers
{
    [Route("api/mark"), ApiController]
    [Authorize]
    public class MarkController : ControllerBase
    {
        private readonly IMarkService markService;

        public MarkController(IMarkService markService)
        {
            this.markService = markService;
        }

        [HttpGet("{messageId}")]
        public Task<sbyte> GetAsync(long messageId)
        {
            return markService.GetOwnAsync(User.GetId(), messageId);
        }

        [HttpPost("{messageId}/{value}")]
        [HttpPut("{messageId}/{value}")]
        public async Task<IActionResult> SetAsync(long messageId, sbyte value)
        {
            var markDto = new MarkDto() { UserId = User.GetId(), MessageId = messageId, Value = value };

            await markService.SetAsync(markDto);

            return Ok();
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteAsync(long messageId)
        {
            var markDto = new MarkDto() { UserId = User.GetId(), MessageId = messageId, Value = 0 };

            await markService.SetAsync(markDto);

            return Ok();
        }
    }
}
