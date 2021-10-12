using ForumTask.BLL.Interfaces;
using ForumTask.PL.Extensions;
using ForumTask.PL.Filters;
using ForumTask.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ForumTask.PL.Controllers {
    [Route("api/topic")]
    [ApiController]
    [ModelValidFilter]
    [BllExceptionFilter]
    public class TopicController : ControllerBase {
        private readonly ITopicService topicServ;

        public TopicController(ITopicService topicService) {
            topicServ = topicService;
        }

        [HttpGet("{topicId}")]
        public TopicViewModel Get(ulong topicId)
            => new(topicServ.Get(topicId));

        [HttpGet("pageCount")]
        public int GetPageCount()
            => topicServ.GetPagesCount();

        [HttpGet]
        public IEnumerable<TopicViewModel> GetTopNew(uint page, string searchTitle = "")
            => topicServ.GetTopNew(page, searchTitle).Select(dto => new TopicViewModel(dto));

        [Authorize]
        [HttpPost]
        public ulong Create(TopicCreateModel model)
            => topicServ.Create(model.Title, model.Message, User.GetId());

        [Authorize]
        [HttpPut("{topicId}")]
        public IActionResult Rename(ulong topicId, [MaxLength(60)] string newTitle) {
            topicServ.Rename(topicId, newTitle, User.GetId());
            return Ok();
        }

        [Authorize]
        [HttpDelete("{topicId}")]
        public IActionResult Delete(ulong topicId) {
            topicServ.Delete(topicId, User.GetId());
            return Ok();
        }
    }
}
