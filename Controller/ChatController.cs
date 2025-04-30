using apicampusjob.AttributeExtend;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Service;
using Microsoft.AspNetCore.Mvc;

namespace apicampusjob.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DbpCert]

    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send-message")]
        [DbpCert]

        public IActionResult SendMessage([FromBody] SendMessageRequest request)
        {
            var response = _chatService.SendMessage(request);
            return Ok(response);
        }

        [HttpPost("get-messages")]
        [DbpCert]

        public IActionResult GetMessages([FromBody] GetMessagesByConversation request)
        {
            var response = _chatService.GetConversationMessages(request);
            return Ok(response);
        }
    }
}
