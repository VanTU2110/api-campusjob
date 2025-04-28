using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using AutoMapper;

namespace apicampusjob.Service
{
    public interface ICVService
    {
        BaseResponse InsertCV (InsertCVRequest request);
        BaseResponseMessageItem<CVDTO> GetCVByStudentUui (GetCVByStudenUuidRequest request);
        BaseResponseMessage<CVDTO> GetCVByUuid (string uuid);
    }
    public class CVService : BaseService, ICVService
    {
        public IStudentRepository _studentRepository;
        public ICVRepository _cvRepository;
        public CVService(DBContext dbContext, IMapper mapper, IConfiguration configuration, ICVRepository cvRepository,IStudentRepository studentRepository) : base(dbContext, mapper, configuration)
        {
            _cvRepository = cvRepository;
            _studentRepository = studentRepository;
        }

        public BaseResponseMessageItem<CVDTO> GetCVByStudentUui(GetCVByStudenUuidRequest request)
        {
            var response = new BaseResponseMessageItem<CVDTO>();
            var lstCV = _cvRepository.GetCVByStudentUuid(request);
            if (lstCV == null)
            {
                throw new ErrorException(ErrorCode.CV_NOT_FOUND);
            }
            var detailLstCV = _mapper.Map<List<CVDTO>>(lstCV);
            response.Data = detailLstCV;
            return response;
        }

        public BaseResponseMessage<CVDTO> GetCVByUuid(string uuid)
        {
            var response = new BaseResponseMessage<CVDTO>();
            var cv = _cvRepository.GetDetailCV(uuid);
            if (cv == null)
            {
                throw new ErrorException(ErrorCode.CV_NOT_FOUND);
            }
            var detailCVDTO = _mapper.Map<CVDTO>(cv);
            response.Data = detailCVDTO;
            return response;
        }

        public BaseResponse InsertCV(InsertCVRequest request)
        {
            var newCV = new StudentCv
            {
                StudentUuid = request.StudentUuid,
                CloudinaryPublicId = request.CloudinaryPublicId,
                Url = request.Url,
                UploadAt = DateTime.Now,
            };
            return ExecuteInTransaction(() =>
            {
                 return new BaseResponseMessage<CVDTO>
                {
                    Data = _mapper.Map<CVDTO>(_cvRepository.CreateItem(newCV))
                };
            });
        }
    }
}
