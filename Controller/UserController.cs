using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using apicampusjob.AttributeExtend;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Service;
using Swashbuckle.AspNetCore.Annotations;
using apicampusjob.Controllers;
using static apicampusjob.Service.UserServicecs;
using apicampusjob.Enums;

namespace secondhand_market.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IMapper mapper) : BaseController
    {
        private readonly IUserService _userService= userService;
        private readonly DBContext _context;
        [HttpPost("detail-user")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<UserDTO>), description: "DetailUser Response")]
        public async Task<IActionResult> DetailUser([FromBody] UuidRequest request)
        {
           return ProcessRequest((token) =>
           {
               var response = _userService.GetDetailUserByUuid(request.Uuid);
               return Ok(response);
           },_context);
        }
        [HttpPost("update-status")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "Update Status Response")]
        public async Task<IActionResult> UpdateStatus([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();


                _userService.UpdateStatus(request.Uuid);


                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }


    }
}
