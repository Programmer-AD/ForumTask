using ForumTask.BLL.Interfaces;
using ForumTask.PL.Extensions;
using ForumTask.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumTask.PL.Controllers
{
    [Route("api/topic"), ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService topicService;

        public TopicController(ITopicService topicService)
        {
            this.topicService = topicService;
        }

        [HttpGet("{topicId}")]
        public async Task<TopicViewModel> GetAsync(long topicId)
        {
            var topicDto = await topicService.GetAsync(topicId);

            var topicViewModel = new TopicViewModel(topicDto);

            return topicViewModel;
        }

        [HttpGet("pageCount")]
        public Task<int> GetPageCountAsync()
        {
            return topicService.GetPagesCountAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<TopicViewModel>> GetTopNewAsync(int page, string searchTitle = "")
        {
            var topicDtos = await topicService.GetTopNewAsync(page, searchTitle);

            var topicViewModels = topicDtos.Select(dto => new TopicViewModel(dto));

            return topicViewModels;
        }

        [Authorize]
        [HttpPost]
        public Task<long> CreateAsync(TopicCreateModel model)
        {
            return topicService.CreateAsync(model.Title, model.Message, User.GetId());
        }

        [Authorize]
        [HttpPut("{topicId}")]
        public async Task<IActionResult> RenameAsync(long topicId, TopicRenameModel rename)
        {
            await topicService.RenameAsync(topicId, rename.NewTitle, User.GetId());

            return Ok();
        }

        [Authorize]
        [HttpDelete("{topicId}")]
        public async Task<IActionResult> DeleteAsync(long topicId)
        {
            await topicService.DeleteAsync(topicId, User.GetId());

            return Ok();
        }
    }
}
