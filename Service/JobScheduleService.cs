using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using apicampusjob.Enums;
using AutoMapper;
using apicampusjob.Extensions;
using apicampusjob.Utils;

namespace apicampusjob.Service
{
    public interface IJobScheduleService
    {
        BaseResponseMessagePage<ScheduleInfoCatalogDTO> GetPageListSchedule(GetPageListSchedule request);
        BaseResponse InsertSchedule(UpsertScheduleRequest request);
        BaseResponse UpdateSchedule(UpsertScheduleRequest request, TokenInfo token);
        BaseResponseMessageItem<ScheduleInfoCatalogDTO> GetDetailSchedule(GetListJobScheduleByJobUuid request);
        BaseResponse DeleteSchedule(string uuid);
    }
    public class JobScheduleService : BaseService, IJobScheduleService

    {
        public IJobScheduleRepository _jobScheduleRepository;
        public IJobRepository _jobRepository;

        public JobScheduleService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IJobScheduleRepository jobScheduleRepository,IJobRepository jobRepository) : base(dbContext, mapper, configuration)
        {
            _jobScheduleRepository = jobScheduleRepository;
            _jobRepository = jobRepository;
        }

        public BaseResponse DeleteSchedule(string uuid)
        {
            var response = new BaseResponse();
            var schedule = _jobScheduleRepository.GetJobScheduleByUuid(uuid);
            if (schedule == null)
            {
                throw new ErrorException(ErrorCode.SCHEDULE_NOT_FOUND);
            }
            // Xoá trong transaction để đảm bảo an toàn
            return ExecuteInTransaction(() =>
            {
                _jobScheduleRepository.DeleteItem(schedule);
                return response;
            });
        }

        public BaseResponseMessageItem<ScheduleInfoCatalogDTO> GetDetailSchedule(GetListJobScheduleByJobUuid request)
        {
            var response = new BaseResponseMessageItem<ScheduleInfoCatalogDTO>();
            if(_jobRepository.GetJobByUuid(request.JobUuid) == null)
            {
                throw new ErrorException(ErrorCode.REPORT_NOT_FOUND);
            }
            var lstSchedule = _jobScheduleRepository.GetJobScheduleByJobUuid(request);
            if(lstSchedule == null)
            {
                throw new ErrorException(ErrorCode.SCHEDULE_NOT_FOUND);
            }
            var detailLstSchedule = _mapper.Map<List<ScheduleInfoCatalogDTO>>(lstSchedule);
            response.Data = detailLstSchedule;
            return response;
        }

        public BaseResponseMessagePage<ScheduleInfoCatalogDTO> GetPageListSchedule(GetPageListSchedule request)
        {
            var response = new BaseResponseMessagePage<ScheduleInfoCatalogDTO>();
            var lstSchedule = _jobScheduleRepository.GetPageListJobSchedule(request);
            var count = _jobScheduleRepository.Count(request);
            if(lstSchedule != null && count >0)
            {
                var result = lstSchedule.TakePage(request.Page, request.PageSize);
                var lstScheduleDTO = _mapper.Map<List<ScheduleInfoCatalogDTO>>(result);
                response.Data.Items = lstScheduleDTO;
                response.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return response;
        }

        public BaseResponse InsertSchedule(UpsertScheduleRequest request)
        {
           if(_jobRepository.GetJobByUuid(request.Job_Uuid) == null)
            {
                throw new ErrorException(ErrorCode.REPORT_NOT_FOUND);

            }
            var newSchedule = new JobSchedule
            {
                JobUuid = request.Job_Uuid,
                DayOfWeek = request.Day_of_week,
                StartTime = request.Start_time,
                EndTime = request.End_time,

            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<ScheduleInfoCatalogDTO>
                {
                    Data = _mapper.Map<ScheduleInfoCatalogDTO>(_jobScheduleRepository.CreateItem(newSchedule))
                };
            });
        }

        public BaseResponse UpdateSchedule(UpsertScheduleRequest request, TokenInfo token)
        {
            var response = new BaseResponse();
            var oldSchedule = _jobScheduleRepository.GetJobScheduleByUuid(request.Uuid);
            if (_jobRepository.GetJobByUuid(request.Job_Uuid) == null)
            {
                throw new ErrorException(ErrorCode.REPORT_NOT_FOUND);
            }
            if (oldSchedule == null)
            {
                throw new ErrorException(ErrorCode.SCHEDULE_NOT_FOUND);
            }
            oldSchedule.DayOfWeek = request.Day_of_week;
            oldSchedule.StartTime = request.Start_time;
            oldSchedule.EndTime = request.End_time;
            _jobScheduleRepository.UpdateItem(oldSchedule);
            return response;
        }
    }
}
