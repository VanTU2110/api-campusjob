using apicampusjob.AttributeExtend;
using apicampusjob.Controllers;
using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;
using apicampusjob.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apicampusjob.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [DbpCert]
    public class UserWarningController(IUserWarningService userWarningService,IMapper mapper) : BaseController
    {
        private readonly IUserWarningService _userWarningService = userWarningService;
        private readonly DBContext _context;
        [HttpPost("get-page-list-warning")]
        [DbpCert]
        public async Task<IActionResult> GetPageListWarning([FromBody] GetPageListWarning request)
        {
            return ProcessRequest((token) =>
            {
                var response = _userWarningService.GetPageListWarning(request);
                return Ok(response);
            }, _context);
        }
        [HttpPost("create-warning")]
        [DbpCert]
        public async Task<IActionResult> CreateWarning([FromBody] CreateWarning request)
        {
            return ProcessRequest((tokem) =>
            {
                var response = _userWarningService.CreateWarning(request);
                return Ok(response);
            },_context);
        }
    }
}
