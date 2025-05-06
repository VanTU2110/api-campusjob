using apicampusjob.AttributeExtend;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Service;
using Microsoft.AspNetCore.Mvc;
using apicampusjob.Databases.TM;
namespace apicampusjob.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DbpCert]

    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;
        private readonly DBContext _context;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send-message")]
        [DbpCert]
        public IActionResult SendMessage([FromBody] SendMessageRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _chatService.SendMessage(request);
                return Ok(response);
            }, _context);
            
        }

        [HttpPost("get-messages")]
        [DbpCert]

        public IActionResult GetMessages([FromBody] GetMessagesByConversation request)
        {
            return ProcessRequest((token) =>
            {
                var response = _chatService.GetConversationMessages(request);
                return Ok(response);
            }, _context);
           
        }
    }
}
