using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForumTask.PL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumTask.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ForumTask.PL.Models;
using ForumTask.PL.Extensions;
using System.ComponentModel.DataAnnotations;

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
            messageServ.Add(new() { Text = model.Text, TopicId= model.TopicId, AuthorId = User.GetId() });
            return Ok();
        }

        [Authorize]
        [HttpPut("{messageId}")]
        public IActionResult Edit(long messageId, [MinLength(10)][MaxLength(5000)]string newText) {
            messageServ.Edit(messageId, newText, User.GetId());
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
