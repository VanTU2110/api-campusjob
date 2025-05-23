using AutoMapper;
using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using apicampusjob.Utils;
using secondhand_market.Repository;
using System.ComponentModel.DataAnnotations;

namespace apicampusjob.Service
{
        public interface IAuthService
        {
        BaseResponse RegisterStudent(RegisterAccountRequest request);
        BaseResponse RegisterCompany(RegisterAccountRequest request);
        BaseResponse LogOut(string token);

        BaseResponseMessage<LogInResp> Login(LogInRequest request);
        BaseResponse VerifyUser(VerifyUserRequest request);
    }

    public class AuthService : BaseService, IAuthService
        {
            private readonly IUserRepository _userRepository;
            private readonly ISessionRepository _sessionRepository;
        private readonly IOtpService _otpService;
        public AuthService(DBContext dbContext, IMapper mapper, IConfiguration configuration, ISessionRepository sessionRepository, IUserRepository userRepository, IOtpService otpService) : base(dbContext, mapper, configuration)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _otpService = otpService;
        }
        private LogInResp CheckInfor(LogInRequest request)
            {
                var loginResp = new LogInResp();
                var acc = _userRepository.GetUserByEmail(request.Email);

                if (acc == null)
                {
                    throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
                }


                if (acc.Password != MD5Util.Encrypt(request.Password))
                {
                    throw new ErrorException(ErrorCode.INVALID_PASS);
                }
                if (acc.Status == 0)
                {
                    throw new ErrorException(ErrorCode.ACCOUNT_LOCKED);
                }

            // Populate response
            loginResp.Uuid = acc.Uuid;
                loginResp.Email = acc.Email;
                loginResp.Role = acc.Role;
            loginResp.IsVerify = acc.IsVerify;

                return loginResp;
            }
            public BaseResponse RegisterStudent(RegisterAccountRequest request)
            {
                if (!Validators.IsValidEmail(request.Email))
                {
                    throw new ErrorException(ErrorCode.EMAIL_NOT_VALIDATOR);
                }
                if (!Validators.IsValidPassword(request.Password))
                {
                    throw new ErrorException(ErrorCode.PASSWORD_NOT_VALIDATOR);
                }
                var existingUser = _userRepository.GetUserByEmail(request.Email);
                if (existingUser != null)
                {
                    throw new ErrorException(ErrorCode.EMAIL_IS_USED);
                }
                var newUser = new User
                {
                    Email = request.Email,
                    Password = MD5Util.Encrypt(request.Password),
                    Status = 1,
                };
                return ExecuteInTransaction(() =>
                {
                    return new BaseResponseMessage<UserDTO>
                    {
                        Data = _mapper.Map<UserDTO>(_userRepository.CreateItem(newUser))
                    };
                });
            }
        public BaseResponse RegisterCompany(RegisterAccountRequest request)
        {
            if (!Validators.IsValidEmail(request.Email))
            {
                throw new ErrorException(ErrorCode.EMAIL_NOT_VALIDATOR);
            }
            if (!Validators.IsValidPassword(request.Password))
            {
                throw new ErrorException(ErrorCode.PASSWORD_NOT_VALIDATOR);
            }
            var existingUser = _userRepository.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                throw new ErrorException(ErrorCode.EMAIL_IS_USED);
            }
            var newUser = new User
            {
                Email = request.Email,
                Password = MD5Util.Encrypt(request.Password),
                Role = 1,
                Status = 1,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<UserDTO>
                {
                    Data = _mapper.Map<UserDTO>(_userRepository.CreateItem(newUser))
                };
            });
        }

        public BaseResponseMessage<LogInResp> Login(LogInRequest request)
            {
                var response = new BaseResponseMessage<LogInResp>();
                request.Email = request.Email.Trim().ToLower();
                var LogInRes = CheckInfor(request);
                var _token = TokenManager.getTokenInfoByEmail(request.Email);


                if (_token != null)
                {
                    TokenManager.removeToken(_token.Token);
                }
                var user = _userRepository.GetUserByEmail(LogInRes.Email);
                LogInRes.Uuid =user.Uuid;
                _token = new TokenInfo()
                {
                    Token = Guid.NewGuid().ToString(),
                    Email = LogInRes.Email,
                    UserUuid = LogInRes.Uuid,
                    Role = LogInRes.Role,
                };
                _token.ResetExpired();
                LogInRes.Token = _token.Token;
                response.Data = LogInRes;
                
                TokenManager.addToken(_token);
                TokenManager.clearToken();


                var oldSessions = _sessionRepository.GetListSessionByAccountUuid(LogInRes.Uuid);
                if (oldSessions != null && oldSessions.Count > 0)
                {
                    foreach (var session in oldSessions)
                    {
                        session.Status = 1;
                        _sessionRepository.UpdateItem(session);
                    }
                }

                var newSession = new Sessions()
                {
                    Uuid = _token.Token,
                    UserUuid = _token.UserUuid,
                    TimeLogin = DateTime.Now,
                    Status = 0
                };

                _sessionRepository.CreateItem(newSession);

                return response;
            }

        public BaseResponse VerifyUser(VerifyUserRequest request)
        {
            var response = new BaseResponse();

            // Kiểm tra người dùng tồn tại không
            var user = _userRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
            }

            // Kiểm tra và xác thực OTP
            _otpService.VerifyOtpAsync(request.Email, request.Otp).Wait();
            user.IsVerify = true;
            // Cập nhật trạng thái người dùng thành đã xác thực
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<UserDTO>
                {
                    Data = _mapper.Map<UserDTO>(_userRepository.UpdateItem(user)),
                };
            });
        }
        public BaseResponse LogOut(string token)
        {
            var response = new BaseResponse();
            var tokenInfo = TokenManager.getTokenInfoByToken(token);
            if (tokenInfo != null)
            {

                TokenManager.removeToken(token);


                var oldSessions = _sessionRepository.GetListSessionByAccountUuid(tokenInfo.UserUuid);

                if (oldSessions != null && oldSessions.Count > 0)
                {
                    foreach (var session in oldSessions)
                    {
                        session.Status = 1;
                        session.TimeLogout = DateTime.Now;
                    }


                    _sessionRepository.UpdateItem(oldSessions);
                    return response;
                }
                else
                {
                    throw new ErrorException(ErrorCode.TOKEN_INVALID);
                }
            }
            else
            {
                throw new ErrorException(ErrorCode.TOKEN_INVALID);
            }
        }
    }
}
