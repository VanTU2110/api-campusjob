using apicampusjob.Controllers;
using apicampusjob.Models.Request;
using apicampusjob.Service;
using Microsoft.AspNetCore.Mvc;

namespace apicampusjob.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController : BaseController
    {
        private readonly IOtpService _otpService;

        public OTPController(IOtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("send")]
        public IActionResult SendOtp([FromBody] SendOtpRequest request)
        {
            var otp = _otpService.SendOtpAsync(request.Email);
            return Ok(new { message = "OTP đã được gửi", otp });
        }
    }
}
