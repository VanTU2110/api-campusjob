using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Extensions;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using apicampusjob.Utils;
using AutoMapper;

namespace apicampusjob.Service
{
    public interface ISkillService
    {
        BaseResponseMessagePage<SkillDTO> GetPageListSkill(GetPageListSkillRequest request);
        BaseResponse InsertSkill(UpsertSkillRequest request);
        BaseResponse UpdateSkill(UpsertSkillRequest request, TokenInfo token);
        BaseResponseMessage<SkillDTO> GetSkillByUuid(string uuid);
    }
    public class SkillService : BaseService, ISkillService
    {
        public ISkillRepository _skillRepository ;
        public SkillService(DBContext dbContext, IMapper mapper, IConfiguration configuration,ISkillRepository skillRepository) : base(dbContext, mapper, configuration)
        {
            _skillRepository = skillRepository;
        }

        public BaseResponseMessagePage<SkillDTO> GetPageListSkill(GetPageListSkillRequest request)
        {
            var respone = new BaseResponseMessagePage<SkillDTO>();
            var lstSkill = _skillRepository.GetPageListSkill(request);
            var count = _skillRepository.Count(request);
            if (lstSkill != null && count > 0)
            {
                var result = lstSkill.TakePage(request.Page, request.PageSize);
                var lstSkillDTO = _mapper.Map<List<SkillDTO>>(result);

                respone.Data.Items = lstSkillDTO;
                respone.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return respone;
        }

        public BaseResponseMessage<SkillDTO> GetSkillByUuid(string uuid)
        {
            var response = new BaseResponseMessage<SkillDTO>();
            var skill = _skillRepository.GetSkillDetailByUuid(uuid);
            if (skill == null)
            {
                throw new ErrorException(ErrorCode.SKILL_NOT_FOUND);
            }
            var detailSkillDTO = _mapper.Map<SkillDTO>(skill);
            response.Data = detailSkillDTO;
            return response;
        }

        public BaseResponse InsertSkill(UpsertSkillRequest request)
        {
            if(_skillRepository.IsSkillNameExisted(request.Name))
{
                return new BaseResponse
                {
                    error = new BaseResponse.Error
                    {
                        Code = ErrorCode.CONFLICT, // hoặc bạn có thể tạo ErrorCode riêng ví dụ: SKILL_NAME_EXISTED
                        Message = "Tên kỹ năng đã tồn tại trong hệ thống."
                    }
                };
            }

            var newSkill = new Skills
            {
                Name = request.Name,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<SkillDTO>
                {
                    Data = _mapper.Map<SkillDTO>(_skillRepository.CreateItem(newSkill)),
                };
            });
        }

        public BaseResponse UpdateSkill(UpsertSkillRequest request, TokenInfo token)
        {
            var response = new BaseResponse();
            var oldSkill = _skillRepository.GetSkillDetailByUuid(request.Uuid);
            if (oldSkill == null)
            {
                throw new ErrorException(ErrorCode.SKILL_NOT_FOUND);
            }
            oldSkill.Name = request.Name;
            _skillRepository.UpdateItem(oldSkill);
            return response;
        }
    }
}
