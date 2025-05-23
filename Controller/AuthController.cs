﻿using AutoMapper;
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
using apicampusjob.Models.BaseRequest;
using apicampusjob.Enums;

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
        // Xác thực người dùng (verify)
        [HttpPost("verify-user")]
        [DbpCert]
        public IActionResult VerifyUser([FromBody] VerifyUserRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _authservice.VerifyUser(request);
                return Ok(response);
            }, _context);
           
        }
        [HttpPost("logout")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "LogOut Response")]
        public async Task<IActionResult> LogOut(DpsParamBase request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();


                _authservice.LogOut(getTokenInfo(_context).Token);
                
                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }

    }
}
