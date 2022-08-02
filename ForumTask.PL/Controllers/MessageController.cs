using ForumTask.BLL.DTO;
using ForumTask.BLL.Interfaces;
using ForumTask.PL.Extensions;
using ForumTask.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumTask.PL.Controllers
{
    [Route("api/message"), ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet("topic{topicId}/pageCount")]
        public Task<int> GetPageCount(long topicId)
        {
            return messageService.GetPagesCountAsync(topicId);
        }

        [HttpGet("topic{topicId}")]
        public async Task<IEnumerable<MessageViewModel>> GetTopOldAsync(long topicId, int page)
        {
            var messageDtos = await messageService.GetTopOldAsync(topicId, page);

            var messageViewModels = messageDtos.Select(dto => new MessageViewModel(dto));

            return messageViewModels;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAsync(MessageAddModel model)
        {
            var messageDto = new MessageDto() { Text = model.Text, TopicId = model.TopicId, AuthorId = User.GetId() };

            await messageService.AddAsync(messageDto);

            return Ok();
        }

        [Authorize]
        [HttpPut("{messageId}")]
        public async Task<IActionResult> EditAsync(long messageId, MessageEditModel model)
        {
            await messageService.EditAsync(messageId, model.NewText, User.GetId());

            return Ok();
        }

        [Authorize]
        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteAsync(long messageId)
        {
            await messageService.DeleteAsync(messageId, User.GetId());

            return Ok();
        }
    }
}
