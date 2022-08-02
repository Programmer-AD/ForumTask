using System.Collections.Generic;
using System.Linq;
using ForumTask.BLL.Interfaces;
using ForumTask.PL.Extensions;
using ForumTask.PL.Filters;
using ForumTask.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumTask.PL.Controllers
{
    [Route("api/topic")]
    [ApiController]
    [ModelValidFilter]
    [BllExceptionFilter]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService topicServ;

        public TopicController(ITopicService topicService)
        {
            topicServ = topicService;
        }

        [HttpGet("{topicId}")]
        public TopicViewModel Get(long topicId)
        {
            return new(topicServ.GetAsync(topicId));
        }

        [HttpGet("pageCount")]
        public int GetPageCount()
        {
            return topicServ.GetPagesCountAsync();
        }

        [HttpGet]
        public IEnumerable<TopicViewModel> GetTopNew(int page, string searchTitle = "")
        {
            return topicServ.GetTopNewAsync(page, searchTitle).Select(dto => new TopicViewModel(dto));
        }

        [Authorize]
        [HttpPost]
        public long Create(TopicCreateModel model)
        {
            return topicServ.CreateAsync(model.Title, model.Message, User.GetId());
        }

        [Authorize]
        [HttpPut("{topicId}")]
        public IActionResult Rename(long topicId, TopicRenameModel rename)
        {
            topicServ.RenameAsync(topicId, rename.NewTitle, User.GetId());
            return Ok();
        }

        [Authorize]
        [HttpDelete("{topicId}")]
        public IActionResult Delete(long topicId)
        {
            topicServ.DeleteAsync(topicId, User.GetId());
            return Ok();
        }
    }
}
