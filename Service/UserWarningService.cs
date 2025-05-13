using apicampusjob.Databases.TM;
using apicampusjob.Extensions;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using AutoMapper;

namespace apicampusjob.Service
{
    public interface IUserWarningService
    {
        BaseResponse CreateWarning(CreateWarning request);
        BaseResponseMessagePage<UserWarningDTO> GetPageListWarning(GetPageListWarning request);
    }
    public class UserWarningService : BaseService, IUserWarningService
    {
        private readonly IUserWarningRepository _userWarningRepository;
        public UserWarningService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IUserWarningRepository userWarningRepository) : base(dbContext, mapper, configuration)
        {
            _userWarningRepository = userWarningRepository;
        }

        public BaseResponse CreateWarning(CreateWarning request)
        {
            var newWarning = new UserWarning
            {
                TargetType = request.TargetType,
                TargetUuid = request.TargetUuid,
                Messages = request.Messages,
                CreatedAt = DateTime.Now,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<UserWarningDTO>
                {
                    Data = _mapper.Map<UserWarningDTO>(_userWarningRepository.CreateItem(newWarning))
                };
            });
        }

        public BaseResponseMessagePage<UserWarningDTO> GetPageListWarning(GetPageListWarning request)
        {
            var response = new BaseResponseMessagePage<UserWarningDTO>();
            var lstWarning = _userWarningRepository.GetPageListWarning(request);
            int count = lstWarning.Count;
            if (lstWarning != null && count > 0)
            {
                var result = lstWarning.OrderByDescending(x => x.CreatedAt).TakePage(request.Page, request.PageSize);
                var lstWarningDTO = _mapper.Map<List<UserWarningDTO>>(result);

                response.Data.Items = lstWarningDTO;
                response.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return response;
        }
    }
}
