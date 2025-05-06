using AutoMapper;
using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using secondhand_market.Repository;

namespace apicampusjob.Service
{
    public class UserServicecs
    {
        public interface IUserService
        {
            User UpdateStatus(string uuid);
            BaseResponseMessage<UserDTO> GetDetailUserByUuid(string uuid);
            BaseResponseMessage<UserDTO> GetDetailUserByEmail(string email);
        }
        public class UserService : BaseService, IUserService
        {
            private readonly IUserRepository _userRepository;
            private readonly ISessionRepository _sessionRepository;

            public UserService(DBContext dbContext, IMapper mapper, IConfiguration configuration, ISessionRepository sessionRepository, IUserRepository userRepository) : base(dbContext, mapper, configuration)
            {
                _userRepository = userRepository;
                _sessionRepository = sessionRepository;

            }

           
                public BaseResponseMessage<UserDTO> GetDetailUserByEmail(string email)
            {
                var response = new BaseResponseMessage<UserDTO>();
                var user = _userRepository.GetUserByEmail(email);
                if (user == null)
                {
                    throw new Exception("Khong co user nay");
                }
                var detailUserDTO = _mapper.Map<UserDTO>(user);
                response.Data = detailUserDTO;
                return response;
            }

            public BaseResponseMessage<UserDTO> GetDetailUserByUuid(string uuid)
            {
                var response = new BaseResponseMessage<UserDTO>();
                var user = _userRepository.GetUserByUuid(uuid);
                if (user == null)
                {
                    throw new Exception("Khong co user nay");
                }
                var detailUserDTO = _mapper.Map<UserDTO>(user);
                response.Data = detailUserDTO;
                return response;
            }

            public User UpdateStatus(string uuid)
            {
                return ExecuteInTransaction(() =>
                {
                    var user = _userRepository.GetUserByUuid(uuid);
                    if (user == null)
                    {
                        throw new ErrorException(ErrorCode.USER_NOT_FOUND);
                    }

                    user.Status = (sbyte)(user.Status == 1 ? 0 : 1);

                    return _userRepository.UpdateItem(user);
                });
            }
        }
    }
}
