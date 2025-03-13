using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apicampusjob.AttributeExtend;
using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Service;
using Swashbuckle.AspNetCore.Annotations;
using apicampusjob.Controllers;

namespace apicampusjob.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authservice;
        private readonly DBContext _context;
        private readonly ILogger<AuthController> _logger;
        public AuthController(DBContext context, ILogger<AuthController> logger, IAuthService authService)
        {

            _context = context;
            _logger = logger;
            _authservice = authService ;
        }
        [HttpPost("register-student")]
        public IActionResult RegisterStudent([FromBody] RegisterAccountRequest request)
        {
            return ProcessRequest(() =>
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _authservice.RegisterStudent(request);
                return Ok(response);
            }, _context);
        }
        [HttpPost("register-company")]
        public IActionResult RegisterCompany([FromBody] RegisterAccountRequest request)
        {
            return ProcessRequest(() =>
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _authservice.RegisterCompany(request);
                return Ok(response);
            }, _context);
        }
        [HttpPost("login")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(LogInResp), description: "LogIn Response")]
        public async Task<IActionResult> LogIn(LogInRequest request)
        {
            var response = new BaseResponseMessage<LogInResp>();
            try
            {
                response = _authservice.Login(request);
                return Ok(response);
            }
            catch (ErrorException ex)
            {
                response.error.SetErrorCode(ex.Code);
                _logger.LogError(ex.Message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.error.SetErrorCode(Enums.ErrorCode.SYSTEM_ERROR);
                _logger.LogError(ex.Message);

                return StatusCode(500, response);
            }
        }
    }
}
