using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Extensions;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using AutoMapper;
using static apicampusjob.Enums.EnumDatabase;

namespace apicampusjob.Service
{
    public interface IApplicationService
    {
        BaseResponse ApplyJob(ApplyJobRequest request);
        BaseResponse CancellationofApplication(CancellationofApplicationRquest request);
        BaseResponse UpdateStatus(UpdateStatusRequest request );
        BaseResponseMessagePage<ApplicationDTO> GetPageListApplyByJobUuid(GetPageListApplyByJobUuid request);
        BaseResponseMessagePage<ApplicationDTO> GetPageListApplyByStudentUuid(GetPageListApplyByStudentUuid request);
    }
    public class ApplicationService : BaseService, IApplicationService
    {
        public IStudentRepository _studentRepository;
        public IJobRepository _jobRepository;
        public IApplicationRepository _applicationRepository;
        public ApplicationService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IJobRepository jobRepository, IApplicationRepository applicationRepository,IStudentRepository studentRepository) : base(dbContext, mapper, configuration)
        {
            _studentRepository = studentRepository;
            _jobRepository = jobRepository;
            _applicationRepository = applicationRepository;
        }

        public BaseResponse ApplyJob(ApplyJobRequest request)
        {
            if (_studentRepository.GetStudentInforByStudentUuid(request.StudentUuid) == null)
            {
                throw new ErrorException(ErrorCode.STUDENT_NOT_FOUND);

            }
            if (_jobRepository.GetJobByUuid(request.JobUuid)== null)
            {
                throw new ErrorException(ErrorCode.JOB_NOT_FOUND);
            }
            var newApply = new Applications
            {
                StudentUuid = request.StudentUuid,
                JobUuid = request.JobUuid,
                CoverLeter = request.CoverLetter,

            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<ApplicationDTO>
                {
                    Data = _mapper.Map<ApplicationDTO>(_applicationRepository.CreateItem(newApply))
                };
            });
        }

        public BaseResponse CancellationofApplication(CancellationofApplicationRquest request)
        {
            var application = _applicationRepository.GetApplicationsByUuid(request.Application_uuid);
            if (application == null)
            {
                throw new ErrorException(ErrorCode.APPLICATION_NOT_FOUND);
            }

            var currentStatus = Enum.Parse<ApplicationStatus>(application.Status,ignoreCase:true);

            // Chỉ cho phép hủy nếu đang ở trạng thái cho phép
            if (currentStatus is ApplicationStatus.Accepted or
                                ApplicationStatus.Hired or
                                ApplicationStatus.Rejected or
                                ApplicationStatus.Cancelled)
            {
                throw new ErrorException(ErrorCode.CANNOT_CANCEL_APPLICATION);
            }

            application.Status = ApplicationStatus.Cancelled.ToString();
            application.IsActive = false;
            application.UpdatedAt = DateTime.UtcNow;

            return ExecuteInTransaction(() =>
            {
                var updated = _applicationRepository.UpdateItem(application);
                return new BaseResponseMessage<ApplicationDTO>
                {
                    Data = _mapper.Map<ApplicationDTO>(updated)
                };
            });
        }

        public BaseResponseMessagePage<ApplicationDTO> GetPageListApplyByJobUuid(GetPageListApplyByJobUuid request)
        {
            var response = new BaseResponseMessagePage<ApplicationDTO>();
            var lstApply = _applicationRepository.GetPageListApplyByJobUuid(request);
            var count = _applicationRepository.CountApplyByJobUuid(request);
            if(lstApply != null && count> 0) {
                var result = lstApply.OrderByDescending(x => x.AppliedAt).TakePage(request.Page, request.PageSize);
                var lstApplyDTO = _mapper.Map<List<ApplicationDTO>>(result);
                response.Data.Items = lstApplyDTO;
                response.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return response;
        }

        public BaseResponseMessagePage<ApplicationDTO> GetPageListApplyByStudentUuid(GetPageListApplyByStudentUuid request)
        {
            var response = new BaseResponseMessagePage<ApplicationDTO>();
            var lstApply = _applicationRepository.GetPageListApplyByStudentUuid(request);
            var count = _applicationRepository.CountApplyByStudentUuid(request);
            if (lstApply != null && count > 0)
            {
                var result = lstApply.OrderByDescending(x => x.AppliedAt).TakePage(request.Page, request.PageSize);
                var lstApplyDTO = _mapper.Map<List<ApplicationDTO>>(result);
                response.Data.Items = lstApplyDTO;
                response.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return response;
        }
        private readonly Dictionary<ApplicationStatus, ApplicationStatus[]> _validTransitions = new()
    {
        { ApplicationStatus.Pending,       new[] { ApplicationStatus.Interviewing, ApplicationStatus.Rejected } },
        { ApplicationStatus.Interviewing,  new[] { ApplicationStatus.Accepted, ApplicationStatus.Rejected } },
        { ApplicationStatus.Accepted,      new[] { ApplicationStatus.Hired } },
        { ApplicationStatus.Rejected,      Array.Empty<ApplicationStatus>() },
        { ApplicationStatus.Hired,         Array.Empty<ApplicationStatus>() },
    };
        public BaseResponse UpdateStatus(UpdateStatusRequest request)
        {
            var application = _applicationRepository.GetApplicationsByUuid(request.Uuid);
            if (application == null)
            {
                throw new ErrorException(ErrorCode.APPLICATION_NOT_FOUND);
            }

            // Convert string -> enum, có thể throw nếu sai tên enum
            if (!Enum.TryParse<ApplicationStatus>(request.Status, true, out var newStatus))
            {
                throw new ErrorException(ErrorCode.INVALID_APPLICATION_STATUS);
            }

            var currentStatus = Enum.Parse<ApplicationStatus>(application.Status, ignoreCase: true);

            // Kiểm tra transition có hợp lệ không
            if (!_validTransitions.TryGetValue(currentStatus, out var allowedStatuses) ||
                !allowedStatuses.Contains(newStatus))
            {
                throw new ErrorException(ErrorCode.INVALID_STATUS_TRANSITION);
            }

            // Cập nhật trạng thái và thời gian
            application.Status = newStatus.ToString().ToLower();
            application.UpdatedAt = DateTime.UtcNow;

            return ExecuteInTransaction(() =>
            {
                var updatedApplication = _applicationRepository.UpdateItem(application);
                return new BaseResponseMessage<ApplicationDTO>
                {
                    Data = _mapper.Map<ApplicationDTO>(updatedApplication)
                };
            });
        }
    }
}
