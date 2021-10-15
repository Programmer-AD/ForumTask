using ForumTask.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using ForumTask.PL.Extensions;
using ForumTask.PL.Filters;

namespace ForumTask.PL.Controllers {
    [Route("api/mark")]
    [ApiController]
    [Authorize]
    [ModelValidFilter]
    public class MarkController : ControllerBase {
        private readonly IMarkService markServ;

        public MarkController(IMarkService markService) {
            markServ = markService;
        }

        [HttpGet("{messageId}")]
        public sbyte Get(long messageId) 
            => markServ.GetOwn(User.GetId(),messageId);

        [HttpPost("{messageId}/{value}")]
        [HttpPut("{messageId}/{value}")]
        public IActionResult Set(long messageId, sbyte value) {
            markServ.Set(new() { UserId = User.GetId(), MessageId = messageId, Value = value });
            return Ok();
        }

        [HttpDelete("{messageId}")]
        public IActionResult Delete(long messageId) {
            markServ.Set(new() { UserId = User.GetId(), MessageId = messageId, Value = 0 });
            return Ok();
        }
    }
}
