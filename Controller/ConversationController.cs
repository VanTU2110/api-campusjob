using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Models.DataInfo;
using apicampusjob.Service;
using Microsoft.AspNetCore.Mvc;
using apicampusjob.AttributeExtend;

namespace apicampusjob.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [DbpCert]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost("create")]
        public IActionResult CreateConversation([FromBody] CreateConversationRequest request)
        {
            var result = _conversationService.CreateConversation(request);
            return Ok(result);
        }

        [HttpPost("get-by-uuid")]
        public IActionResult GetConversationByUuid([FromBody] GetConversationByUuidRequest request)
        {
            var result = _conversationService.GetConversationByUuid(request.Uuid);
            return Ok(result);
        }
        [HttpPost("list-by-student")]
        public IActionResult GetConversationListByStudent([FromBody] GetConversationListByStudentRequest request)
        {
            var result = _conversationService.GetConversationsByStudent(request.StudentUuid);
            return Ok(result);
        }

        [HttpPost("list-by-company")]
        public IActionResult GetConversationListByCompany([FromBody] GetConversationListByCompanyRequest request)
        {
            var result = _conversationService.GetConversationsByCompany(request.CompanyUuid);
            return Ok(result);
        }


    }
}
