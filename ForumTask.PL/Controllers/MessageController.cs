using ForumTask.BLL.Interfaces;
using ForumTask.PL.Extensions;
using ForumTask.PL.Filters;
using ForumTask.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ForumTask.PL.Controllers {
    [Route("api/message")]
    [ApiController]
    [ModelValidFilter]
    [BllExceptionFilter]
    public class MessageController : ControllerBase {
        private readonly IMessageService messageServ;

        public MessageController(IMessageService messageService) {
            messageServ = messageService;
        }

        [HttpGet("topic{topicId}/pageCount")]
        public int GetPageCount(long topicId)
            => messageServ.GetMessageCount(topicId);

        [HttpGet("topic{topicId}")]
        public IEnumerable<MessageViewModel> GetTopOld(long topicId, int page)
            => messageServ.GetTopOld(topicId, page).Select(dto => new MessageViewModel(dto));

        [Authorize]
        [HttpPost]
        public IActionResult Add(MessageAddModel model) {
            messageServ.Add(new() { Text = model.Text, TopicId = model.TopicId, AuthorId = User.GetId() });
            return Ok();
        }

        [Authorize]
        [HttpPut("{messageId}")]
        public IActionResult Edit(long messageId, MessageEditModel model) {
            messageServ.Edit(messageId, model.NewText, User.GetId());
            return Ok();
        }

        [Authorize]
        [HttpDelete("{messageId}")]
        public IActionResult Delete(long messageId) {
            messageServ.Delete(messageId, User.GetId());
            return Ok();
        }
    }
}
