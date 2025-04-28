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
using CloudinaryDotNet.Actions;

namespace apicampusjob.Service
{
    public interface IJobService
    {
        BaseResponseMessagePage<JobDTO>GetPageListJob(GetPageListJobRequest request);
        BaseResponse InsertJob(UpsertJobRequest request);
        BaseResponse UpdateJob(UpsertJobRequest request, TokenInfo token);
        BaseResponseMessage<JobDTO> GetJobByUuid(string uuid);
    }
    public class JobService : BaseService, IJobService
    {
        public IJobRepository _jobRepository;
        public ICompaniesRepository _companiesRepository;
        public JobService(DBContext dbContext, IMapper mapper, IConfiguration configuration, ICompaniesRepository companiesRepository, IJobRepository jobRepository) : base(dbContext, mapper, configuration)
        {
            _jobRepository = jobRepository;
            _companiesRepository = companiesRepository;
        }

        public BaseResponseMessage<JobDTO> GetJobByUuid(string uuid)
        {
            var response = new BaseResponseMessage<JobDTO>();
            var job = _jobRepository.GetJobByUuid(uuid);
            if (job == null) 
            {
                throw new ErrorException(ErrorCode.JOB_NOT_FOUND);
            }
            var detailJobDTO = _mapper.Map<JobDTO>(job);
            response.Data = detailJobDTO;
            return response;
        }

        public BaseResponseMessagePage<JobDTO> GetPageListJob(GetPageListJobRequest request)
        {
            var respone = new BaseResponseMessagePage<JobDTO>();
            var lstJob = _jobRepository.GetPageListJob(request);
            var count = _jobRepository.Count(request);
            if (lstJob != null && count > 0)
            {
                var result = lstJob.OrderByDescending(x => x.Created).TakePage(request.Page, request.PageSize);
                var lstJobsDTO = _mapper.Map<List<JobDTO>>(result);

                respone.Data.Items = lstJobsDTO;
                respone.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return respone;
        }

        public BaseResponse InsertJob(UpsertJobRequest request)
        {
            if(_companiesRepository.GetCompaniesInforbyUuid(request.CompanyUuid) == null)
            {
                throw new ErrorException(ErrorCode.COMPANY_NOT_FOUND);

            }
            var newJob = new Job
            {
                CompanyUuid = request.CompanyUuid,
                Title = request.Title,
                Description = request.Description,
                JobType = request.JobType,
                SalaryType = request.SalaryType,
                SalaryMin = request.SalaryMin,
                SalaryMax = request.SalaryMax,
                SalaryFixed = request.SalaryFixed,
                Currency = request.Currency,
                Requirements = request.Requirements,
                Created = DateTime.Now,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<JobDTO>
                {
                    Data = _mapper.Map<JobDTO>(_jobRepository.CreateItem(newJob))
                };
            });
        }

        public BaseResponse UpdateJob(UpsertJobRequest request,TokenInfo token)
        {
            var response = new BaseResponse();
            var oldJob = _jobRepository.GetJobByUuid(request.Uuid);
            if (oldJob == null)
            {
                throw new ErrorException(ErrorCode.JOB_NOT_FOUND);
            }
            if(_companiesRepository.GetCompaniesInforbyUuid(request.CompanyUuid) == null)
            {
                throw new ErrorException(ErrorCode.COMPANY_NOT_FOUND);
            }
            oldJob.CompanyUuid = request.CompanyUuid;
            oldJob.Title = request.Title;
            oldJob.Description = request.Description;
            oldJob.JobType = request.JobType;
            oldJob.SalaryType = request.SalaryType;
            oldJob.SalaryMin = request.SalaryMin;
            oldJob.SalaryMax = request.SalaryMax;
            oldJob.SalaryFixed = request.SalaryFixed;
            oldJob.Currency =  request.Currency;
            oldJob.Requirements = request.Requirements;
            oldJob.Updated = DateTime.Now;
            _jobRepository.UpdateItem(oldJob);
            return response;
        }
    }
}
