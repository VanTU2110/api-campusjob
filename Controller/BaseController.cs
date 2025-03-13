
using apicampusjob.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;
using apicampusjob.Databases.TM;
using apicampusjob.Service;
using ESDManagerApi.Queue;
using apicampusjob.Configuaration;
using apicampusjob.Enums;
using apicampusjob.Models.Response;

namespace apicampusjob.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [SwaggerTag("Base Controller")]
    public class BaseController : ControllerBase
    {

        protected TokenInfo getTokenInfo(DBContext _context)
        {
            if (HttpContext.Request.Headers.ContainsKey("Authorization") &&
            HttpContext.Request.Headers["Authorization"][0].StartsWith("Bearer "))
            {
                var token = HttpContext.Request.Headers["Authorization"][0]
                    .Substring("Bearer ".Length);

                var result = TokenManager.getTokenInfoByToken(token);

                if (result != null)
                    return result;

                //load token tu database
                var session = _context.Sessions.Include(x => x.UserUu).Include(x => x.UserUu.Role).Where(x => x.Uuid == token && x.Status == 0).SingleOrDefault();
                if (session != null)
                {


                    var _token = new TokenInfo()
                    {
                        Token = session.Uuid,
                        Email = session.UserUu.Email,
                        UserUuid = session.Uuid,
                        Role = session.UserUu.Role,
                    };

                    TokenManager.addToken(_token);

                    return _token;
                }
            }

            return null;
        }

        protected IActionResult ProcessRequest(Func<TokenInfo, IActionResult> func, DBContext _context)
        {
            var validToken = validateToken(_context);
            if (validToken is null)
            {
                return Unauthorized();
            }


            try
            {
                var result = func(validToken);
                return result;
            }
            catch (ErrorException ex)
            {
                var response = new BaseResponse();
                response.error.SetErrorCode(ex.Code);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse();
                //_logger.LogError(ex.Message, ex);
                response.error.SetErrorCode(ErrorCode.SYSTEM_ERROR, ex.Message);

                return StatusCode(500, response);
            }
        }
        protected IActionResult ProcessRequest(Func<IActionResult> func, DBContext _context)
        {
           

            try
            {
                var result = func();
                return result;
            }
            catch (ErrorException ex)
            {
                var response = new BaseResponse();
                response.error.SetErrorCode(ex.Code);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse();
                //_logger.LogError(ex.Message, ex);
                response.error.SetErrorCode(ErrorCode.SYSTEM_ERROR, ex.Message);

                return StatusCode(500, response);
            }
        }
        protected void logOutAllSession(DBContext _context, string accountUuid)
        {
            //LogOut khoi session
            var session = _context.Sessions.Where(x => x.UserUuid == accountUuid && x.Status == 0).ToList();

            if (session != null && session.Count > 0)
            {
                foreach (var item in session)
                {
                    if (item.Status == 0)
                    {
                        item.TimeLogout = DateTime.Now;
                        item.Status = 1;
                    }
                }

                _context.SaveChanges();
            }
        }
        protected bool DoSendEmail(ILogger _logger, string email, string title, string content)
        {
            return true;

            //return HttpService.sendEmail(_logger, email, title, content);
        }

        protected TokenInfo validateToken(DBContext _context)
        {
            var token = getTokenInfo(_context);
            if (token != null)
            {
                if (token.IsExpired())
                {
                    logOutAllSession(_context, token.UserUuid);
                    return null;
                }
                else
                {
                    token.ResetExpired();
                    return token;
                }
            }
            return null;

        }
    }

}
